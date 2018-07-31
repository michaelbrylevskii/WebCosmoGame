using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Net.WebSockets; 
using System.Threading; 
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.DependencyInjection;
using WebCosmoGame.GameCore;

namespace WebCosmoGame
{ 
    public class Startup 
    {
        Game game;
        Task gameTask;

        public Startup()
        {
            game = new Game();
            game.Init();
            gameTask = game.RunAsync();
        }
        // This method gets called by the runtime. Use this method to add services to the container. 
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940 
        public void ConfigureServices(IServiceCollection services) 
        { 
        } 
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) 
        { 
            if (env.IsDevelopment()) 
            { 
                app.UseDeveloperExceptionPage(); 
            } 
 
            app.UseWebSockets(); 
            app.Map("/connect", ConnectRequestHandler); 
            app.Map("", IndexRequestHandler); 
 
 
            /*app.MapWhen(context => { 
                return context.Request.Query.ContainsKey("id") &&  
                        context.Request.Query["id"] == "5"; 
            }, HandleId);*/ 
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
            app.Use(WebSocketPlayer.Connect);
            /*app.Use(async (context, next) => 
            { 
                if (context.WebSockets.IsWebSocketRequest) 
                { 
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    
                    WebSocketPlayer player = new WebSocketPlayer(webSocket);
                    game.GameMgr.AddPlayer(player);

                    //var buffer = new byte[1024 * 4]; 
                    //await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    //await Echo(context, webSocket); 
                } 
                else 
                { 
                    context.Response.StatusCode = 400; 
                } 
 
            }); */
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