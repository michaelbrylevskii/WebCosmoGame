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
            IsInitialized = true;
            if (OnInitialized != null)
                OnInitialized(this, EventArgs.Empty);
        }

        private void Loop()
        {
            while (true)
            {
                Console.WriteLine("Игровой цикл!");
                Thread.Sleep(UpdatePeriod * 100);
            }
        }
    }
}