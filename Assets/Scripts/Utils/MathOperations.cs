using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Utils
{
    public static class MathOperations
    {
        public static Vector3 GetRandomVector3(float min, float max)
        {
            return new Vector3(
                Random.Range(min, max),
                Random.Range(min, max),
                Random.Range(min, max)
                );
        }

        public static Quaternion GetRandomQuaternion()
        {
            return Quaternion.Euler(
                GetRandomVector3(-180, 180)
                );
        }
    }
}