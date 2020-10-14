using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

namespace TowerDefence.Utilities
{
    public static class MyMathUtil
    {
        public static void DistanceAndDirection(Transform origin, Transform target, out Vector3 direction, out float distance, out Vector3 directionNormalized)
        {
            direction = origin.position - target.position;
            directionNormalized = direction.normalized;
            distance = direction.magnitude;
        }
    }
}
