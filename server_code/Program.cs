using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebCosmoGame
{
    static public class Program
    {
        static public void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        static public IWebHost BuildWebHost(string[] args) => 
            WebHost.CreateDefaultBuilder(args) 
                .UseStartup<Startup>() 
                .Build(); 
    }
}
