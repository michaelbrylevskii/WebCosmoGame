using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebCosmoGame.GameCore
{
    public class Game
    {
        public bool IsInitialized { get; private set; }
        public event EventHandler<EventArgs> OnInitialized;

        public int UpdatePeriod { get; set; }

        public GameMgr GameMgr { get; private set; }

        public Game()
        {

        }

        public void Run()
        {
            if (!IsInitialized)
                Init();
            Loop();
        }

        public async Task RunAsync()
        {
            await Task.Run(() => { Run(); });
        }

        public void Init()
        {
            if (UpdatePeriod <= 0)
                UpdatePeriod = 1000 / 60 * 100;
            
            GameMgr = new GameMgr();

            IsInitialized = true;
            if (OnInitialized != null)
                OnInitialized(this, EventArgs.Empty);
        }

        private void Loop()
        {
            Console.WriteLine("Game");
            while (true)
            {
                Console.WriteLine("Game while");
                GameMgr.Update();
                Thread.Sleep(UpdatePeriod);
            }
        }
    }
}