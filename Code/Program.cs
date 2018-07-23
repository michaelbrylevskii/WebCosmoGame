using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebCosmoGame.Code
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
