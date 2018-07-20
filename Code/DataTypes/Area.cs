using System;

namespace WebCosmoGame.Code.DataTypes
{
    /// <summary>
    /// Координаты двух точек в двумерном пространстве типа float.
    /// Начало прямоугольника X и Y.
    /// Ширина W и высота H.
    /// </summary>
    public struct Area
    {
        public float X;
        public float Y;
        public float W;
        public float H;

        public Area(float xy, float wh)
        {
            X = xy;
            Y = xy;
            W = wh;
            H = wh;
        }
        public Area(float x, float y, float wh)
        {
            X = x;
            Y = y;
            W = wh;
            H = wh;
        }
        public Area(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }
}