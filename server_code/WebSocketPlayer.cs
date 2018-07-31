using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebCosmoGame.GameCore;

namespace WebCosmoGame
{
    public class WebSocketPlayer : Player
    {
        protected WebSocket WebSocket;
        protected Task WebSocketSenderTask;
        protected Task WebSocketReceiverTask;
        protected bool NeedCloseConnection = false;
        protected ConcurrentQueue<string> SendingQueue;
        protected ConcurrentQueue<string> ReceivingQueue;

        public WebSocketPlayer(WebSocket webSocket)
        {
            WebSocket = webSocket;
            SendingQueue = new ConcurrentQueue<string>();
            ReceivingQueue = new ConcurrentQueue<string>();
                    WebSocket.ReceiveAsync(new ArraySegment<byte>(new byte[1024 * 4]), CancellationToken.None);
            WebSocketReceiverTask = WebSocketReceiver();
        }

        Random rnd = new Random();
        
        override public void Update()
        {
            Console.WriteLine("WebSocketPlayer");
            if (ReceivingQueue.Any())
            {
                string message;
                ReceivingQueue.TryDequeue(out message);
            }

            if (rnd.Next(0,5) == 0) {
                string message = $"Выпало 0, а вот и хэш: {rnd.Next(0,1000)}";
                SendingQueue.Enqueue(message);
                Console.WriteLine(message);
            }
        }

        private async Task WebSocketSender()
        {
            await Task.Run(async () => 
            { 
                string message;
                byte[] buffer; 

                while (!NeedCloseConnection) 
                { 
                    while (SendingQueue.Any())
                    {
                        if (SendingQueue.TryDequeue(out message))
                        {
                            buffer = Encoding.Unicode.GetBytes(message);
                            await WebSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Count()), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        else 
                            Thread.Sleep(2);
                    }
                    Thread.Sleep(2);
                } 
            });
        }

        private async Task WebSocketReceiver()
        {
            await Task.Run(async () => 
            { 
                var buffer = new byte[1024 * 4]; 
                string message;
                WebSocketReceiveResult result;
                
                //WebSocketSenderTask = WebSocketSender();

                while (true) 
                { 
                    result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    
                    if (result.CloseStatus.HasValue)
                        break;

                    //message = Encoding.Unicode.GetString(buffer, 0, result.Count);
                    //ReceivingQueue.Enqueue(message);
                }

                /*NeedCloseConnection = true;
                while (WebSocketSenderTask.Status == TaskStatus.Running) 
                    Thread.Sleep(2);*/
                
                await WebSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None); 
            });
        }



        static public async Task Connect(HttpContext context, Func<Task> next)
        { 
            if (context.WebSockets.IsWebSocketRequest) 
            { 
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                
                WebSocketPlayer player = new WebSocketPlayer(webSocket);
                //game.GameMgr.AddPlayer(player);

                //var buffer = new byte[1024 * 4]; 
                //await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                //await Echo(context, webSocket); 
            } 
            else 
            { 
                context.Response.StatusCode = 400; 
            } 

        }
    }
}