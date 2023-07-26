using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevGio
{
    public static class HelperMethods
    {
        public static Vector2 DegToVector(float degree)
        {
            float x = Mathf.Cos(degree * Mathf.Deg2Rad);
            float y = Mathf.Sin(degree * Mathf.Deg2Rad);
            return new Vector2(x, y);
        }

        public static Vector2 ToVector(this float degree) => DegToVector(degree);

        public static float VectorToDeg(Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }

        public static float ToDegree(this Vector2 vector) => VectorToDeg(vector);

        public static Vector2 RotateVector(Vector2 vector, float degrees)
        {
            float vectorAngle = vector.ToDegree();
            float magnitude = vector.magnitude;
            vectorAngle += degrees;
            return DegToVector(vectorAngle) * magnitude;
        }

        public static Vector2 Rotated(this Vector2 vector, float degrees) => RotateVector(vector, degrees);

        public static bool IsOnLayer(LayerMask layerMask, int layer) => layerMask.value == (layerMask | 1 << layer);
    }

}

