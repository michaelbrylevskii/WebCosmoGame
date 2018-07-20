using System;

namespace WebCosmoGame.Code.DataTypes
{
    /// <summary>
    /// Координаты точки в двумерном пространстве типа int
    /// </summary>
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int xy)
        {
            X = xy;
            Y = xy;
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}