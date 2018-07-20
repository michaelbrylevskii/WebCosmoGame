using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebCosmoGame.Code.Core;

namespace WebCosmoGame.Code
{
    /// <summary>
    /// Игровая логика
    /// </summary>
    public class Game
    {
        public IHostingEnvironment HostingEnvironment { get; set; }
        public IConfiguration Configuration { get; set; }

        public bool IsInitialized { get; private set; } = false;
        public event EventHandler<EventArgs> OnInitialized;

        public int UpdatePeriod { get; set; } = 1000 / 60;

        public List<Player> Players = new List<Player>();

        public Game()
        {

        }

        public void Run()
        {
            Init();
            Loop();
        }

        public async Task RunAsync()
        {
            await Task.Run(() => { Run(); });
        }

        private void Init()
        {
            BuildWebHost().RunAsync();
            /*IsInitialized = true;
            if (OnInitialized != null)
                OnInitialized(this, EventArgs.Empty);*/
        }

        private void Loop()
        {
            while (true)
            {
                Console.WriteLine("Игровой цикл!");
                Thread.Sleep(UpdatePeriod * 100);
            }
        }

        public IWebHost BuildWebHost(){
            return WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                HostingEnvironment = hostingContext.HostingEnvironment;
                Configuration = config.Build();
            })
            .ConfigureServices(services =>
            {
                // Сюда добавлять дополнительные сервисы
            })
            .Configure(app =>
            {
                // Конфигурация
                if (HostingEnvironment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseWebSockets();
                app.Map("/connect", ConnectRequestHandler);
                app.Map("", IndexRequestHandler);
            })
            .Build();
        }

        private void IndexRequestHandler(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Page not found!");
            });
        }

        private void ConnectRequestHandler(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await Echo(context, webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }

            });
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}