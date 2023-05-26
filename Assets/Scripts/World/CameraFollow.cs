
using UnityEngine;

namespace PaintRush.World
{
    /// <summary>
    /// Follows and smoothly adjusts the position and rotation of the camera to a target object.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        public static CameraFollow Instance;

        /// <summary>
        /// The target object to follow.
        /// </summary>
        public Transform target;

        /// <summary>
        /// The smoothing speed for the camera movement.
        /// </summary>
        public float smoothSpeed = 0.125f;

        /// <summary>
        /// The offset from the target's position.
        /// </summary>
        public Vector3 locationOffset;

        /// <summary>
        /// The offset from the target's rotation.
        /// </summary>
        public Vector3 rotationOffset;

        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;

            Vector3 desiredPosition = target.position + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
            transform.rotation = smoothedRotation;
        }
    }
}