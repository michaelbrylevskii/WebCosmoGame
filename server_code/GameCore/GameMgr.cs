using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebCosmoGame.GameObjects;
using WebCosmoGame.GameObjects.Ships;

namespace WebCosmoGame.GameCore
{
    // Общая игровая логика
    public class GameMgr
    {
        public List<Player> Players { get; private set; }
        public GameObjMgr GameObjectsManager { get; private set; }

        /*private Core core;*/

        public GameMgr()
        {
            Players = new List<Player>();
            GameObjectsManager = new GameObjMgr();
        }

        public void AddPlayer(Player player) // нужно сделать потокобезопасным
        {
            var ship = new Ship();
            player.Ship = ship;
            AddGameObj(new Ship());
            Players.Add(player);
        }

        public void AddGameObj(GameObj gobj) // нужно сделать потокобезопасным
        {
            GameObjectsManager.Objects.Add(gobj);
        }

        public void Update()
        {
            Console.WriteLine("GameMgr");
            for (int i = 0; i < Players.Count; i++)
            {
                Console.WriteLine("GameMgr for");
                Players[i].Update();
            }
            GameObjectsManager.Update();
        }
    }
}