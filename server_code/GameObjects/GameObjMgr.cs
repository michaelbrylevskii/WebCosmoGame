using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebCosmoGame.GameObjects;

namespace WebCosmoGame.GameObjects
{
    // Логика взаимодействия объектов
    public class GameObjMgr
    {
        public List<GameObj> Objects = new List<GameObj>();

        public void Update()
        {
            foreach (var obj in Objects)
            {
                obj.Update();
            }
        }
    }
}