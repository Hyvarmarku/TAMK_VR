﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public static class Bezier
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
        }

        public enum BezierControlPointMode
        {
            Free,
            Aligned,
            Mirrored
        }
    }
}