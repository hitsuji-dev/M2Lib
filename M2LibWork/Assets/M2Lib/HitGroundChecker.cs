using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace M2Lib
{
    public class HitGroundChecker : MonoBehaviour
    {
        public float radius = 0.5f;
        public float maxDistance = 2f;
        public string fieldLayerName = "Field";
        public Transform[] legs;
        private int layer;
        private List<Transform> legList;

        private void Start()
        {
            layer = 1 << LayerMask.NameToLayer(fieldLayerName);
            legList = legs?.ToList() ?? new List<Transform>();
            if (legList.Any() == false)
            {
                legList.Add(transform);
            }
        }

        public bool Check()
        {
            RaycastHit hit;
            foreach (var leg in legList)
            {
                if (Physics.SphereCast(leg.position + Vector3.up, radius, Vector3.down, out hit, maxDistance, layer))
                {
                    return true;
                }
            }
            return false;
        }
    }
}