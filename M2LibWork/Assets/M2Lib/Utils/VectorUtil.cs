using UnityEngine;

namespace M2Lib.Utils
{
    public static class VectorUtil
    {
        public static float GetRotation(Vector3 from, Vector3 to)
        {
            var targetDir = to - from;
            var rot = Vector3.Angle(targetDir, Vector3.forward);
            return from.x < to.x ? rot : 360f - rot;
        }

        public static int GetRotationEulerY(float diffX, float diffY)
        {
            return (int)(Mathf.Atan2(diffX, diffY) * Mathf.Rad2Deg);
        }
    }
}
