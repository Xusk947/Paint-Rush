using PaintRush.Controller;
using UnityEngine;

namespace PaintRush.World
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.useGravity = false;
            _rigidBody.velocity = new Vector3(0, 0, 25f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null) return;
            Destroy(gameObject);
        }
    }
}