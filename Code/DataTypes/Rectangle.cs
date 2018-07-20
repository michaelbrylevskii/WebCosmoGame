using System;

namespace WebCosmoGame.Code.DataTypes
{
    /// <summary>
    /// Координаты двух точек в двумерном пространстве типа int.
    /// Начало прямоугольника X и Y.
    /// Ширина W и высота H.
    /// </summary>
    public struct Rectangle
    {
        public int X;
        public int Y;
        public int W;
        public int H;

        public Rectangle(int xy, int wh)
        {
            X = xy;
            Y = xy;
            W = wh;
            H = wh;
        }
        public Rectangle(int x, int y, int wh)
        {
            X = x;
            Y = y;
            W = wh;
            H = wh;
        }
        public Rectangle(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }
}