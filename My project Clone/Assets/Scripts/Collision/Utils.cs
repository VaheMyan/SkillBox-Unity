using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace DefultNamespace
{
    public static class Utils
    {
        public static List<Collider> GatAllColliders(this GameObject go)
        {
            return go == null ? null : go.GetComponents<Collider>().ToList(); // veradarcnum a bolor collider-nery
        }

        public static void ToWorldSpaceBox(this BoxCollider box, out float3 center, out float3 halfExtents, // stextsum a collider-neri hamar parametrner
        out quaternion orientation)
        {
            Transform transform = box.transform;
            orientation = transform.rotation;
            center = transform.TransformPoint(box.center);
            var lossyScale = transform.lossyScale;
            var scale = Abs(lossyScale);
            halfExtents = Vector3.Scale(scale, box.size) * 0.5f;
        }

        public static void ToWorldSpaceCapsule(this CapsuleCollider capsule, out float3 point0, out float3 point1,
        out float radius)
        {
            Transform transform = capsule.transform;
            var center = (float3)transform.TransformPoint(capsule.center);
            radius = 0f;
            float height = 0f;
            float3 lossyScale = Abs(transform.lossyScale);
            float3 dir = float3.zero;

            switch(capsule.direction)
            {
                case 0: // x
                    radius = Mathf.Max(a: lossyScale.y, b: lossyScale.z) * capsule.radius;
                    height = lossyScale.x * capsule.height;
                    dir = capsule.transform.TransformDirection(Vector3.right);
                    break;
                case 1: // y
                    radius = Mathf.Max(a: lossyScale.x, b: lossyScale.z) * capsule.radius;
                    height = lossyScale.y * capsule.height;
                    dir = capsule.transform.TransformDirection(Vector3.up);
                    break;
                case 2: // z
                    radius = Mathf.Max(a: lossyScale.x, b: lossyScale.y) * capsule.radius;
                    height = lossyScale.z * capsule.height;
                    dir = capsule.transform.TransformDirection(Vector3.forward);
                    break;
            }

            if(height < radius * 2f)
            {
                dir = Vector3.zero;
            }

            point0 = center + dir * (height * 0.5f - radius);
            point1 = center - dir * (height * 0.5f - radius);
        }

        public static void ToWorldSpaceSphere(this SphereCollider sphere, out float3 center, out float radius)
        {
            Transform transform = sphere.transform;
            center = transform.TransformPoint(sphere.center);
            radius = sphere.radius * Max(v: Abs(transform.lossyScale));
        }

        private static float3 Abs(float3 v)
        {
            return new float3(x: Mathf.Abs(v.x), y: Mathf.Abs(v.y), z: Mathf.Abs(v.z));
        }

        private static float Max(float3 v)
        {
            return Mathf.Max(a: v.x, b: Mathf.Max(a: v.y, b: v.z));
        }
    }
}

