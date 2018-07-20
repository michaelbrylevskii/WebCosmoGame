using System;

namespace WebCosmoGame.Code.DataTypes
{
    /// <summary>
    /// Координаты точки в двумерном пространстве типа float
    /// </summary>
    public struct Vector
    {
        public float X;
        public float Y;

        public Vector(float xy)
        {
            X = xy;
            Y = xy;
        }
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}