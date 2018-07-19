using System;
using System.Threading;

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

        public Game()
        {

        }

        public void Init()
        {
            IsInitialized = true;
            OnInitialized(this, EventArgs.Empty);
        }

        public void Run()
        {
            while (true)
            {
                
                Thread.Sleep(UpdatePeriod);
            }
        }
    }
}