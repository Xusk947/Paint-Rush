using PaintRush.Controller;
using UnityEngine;
using World;

namespace PaintRush.World
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private float _lifeTime = 10f;
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.useGravity = false;
            _rigidBody.velocity = new Vector3(0, 0, 25f);
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if ( _lifeTime < 0 )
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null) return;
            PaintBlock paintBlock = collision.gameObject.GetComponentInParent<PaintBlock>();
            if (paintBlock != null)
            {
                paintBlock.ShaderFill -= .01f;
                Destroy(gameObject);
            }
        }
    }
}