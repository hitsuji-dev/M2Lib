using UnityEngine;
using System.Collections;

namespace M2Engine
{
    public static class VectorUtil
    {
        public static float GetRotation(Vector3 from, Vector3 to)
        {
            var targetDir = to - from;
            var rot = Vector3.Angle(targetDir, Vector3.forward);
            return from.x < to.x ? rot : 360f - rot;
        }
    }
}