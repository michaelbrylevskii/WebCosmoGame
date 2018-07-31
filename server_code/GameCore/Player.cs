using System;
using WebCosmoGame.GameObjects.Ships;

namespace WebCosmoGame.GameCore
{
    /// <summary>
    /// Абстрактный игрок, управляющий объектом IControllable
    /// </summary>
    abstract public class Player
    {
        public string Name { get; set; }

        public Ship Ship;

        abstract public void Update();
    }
}