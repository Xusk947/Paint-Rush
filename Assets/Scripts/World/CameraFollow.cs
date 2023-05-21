using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{
    public class CameraFollow : MonoBehaviour
    {
        public static CameraFollow Instance;

        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 locationOffset;
        public Vector3 rotationOffset;

        private void Awake()
        {
            Instance = this;
        }
        private void FixedUpdate()
        {
            if (target == null) return;
            Vector3 desiredPosition = target.position + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }
    }
}