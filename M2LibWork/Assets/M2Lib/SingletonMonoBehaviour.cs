using System;
using UnityEngine;

namespace M2Lib
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static Lazy<T> InstanceLazy { get; } = new Lazy<T>(() => GameObject.FindObjectOfType<T>());

        public static T Instance => InstanceLazy.Value;
    }
}