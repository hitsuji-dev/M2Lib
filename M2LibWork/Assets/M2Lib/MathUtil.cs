using UnityEngine;

namespace M2Lib
{
    public static class MathUtil
    {
        public static float GetRad(Vector3 p1, Vector3 p2)
            => Mathf.Atan2(p1.y - p2.y, p1.x - p2.x);
    }
}