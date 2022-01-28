using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static Vector2 xy(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector3 WithX(this Vector3 vector, float value)
        {
            return new Vector3(value, vector.y, vector.z);
        }

        public static Vector3 WithY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }

        public static Vector3 WithZ(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, vector.y, value);
        }
    }
}