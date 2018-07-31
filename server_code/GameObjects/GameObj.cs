using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCosmoGame.DataTypes;

namespace WebCosmoGame.GameObjects
{
    abstract public class GameObj
    {
        public Rectangle Rect;
        public int Health;
        public Vector Velocity;

        abstract public void Update();
    }
}