using PaintRush.Input;
using PaintRush.World;
using UnityEngine;
using World;

namespace PaintRush.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [SerializeField]
        private float _straigthSpeed = 1f;
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _reloadSpeed = .5f;
        private float _shootTimer = 100f;

        public float coneRadius = 1f;
        public float coneHeight = 2f;
        public int coneSegments = 16;

        private GameObject _aim;
        
        public PaintHolder PaintHolder { get; private set; }
        public bool Stop = false;
        public bool CanShoot = false;

        public int CollectedBalls
        {
            get { return PaintHolder.PaintBalls.Count; }
        }

        private void Awake()
        {
            Instance = this;
            PaintHolder = transform.Find("PaintHolder").gameObject.AddComponent<PaintHolder>();
            _aim = transform.Find("Aim").gameObject;

        }

        private void FixedUpdate()
        {
            UpdateMovement();
            UpdateShooting();
            //_characterController.Move(new Vector3(axis.x * _speed, 0, _straigth_speed * GameManager.Instance.LevelDifficult));
        }

        private void UpdateMovement()
        {
            if (Stop) return;
            if (transform.position.y > -0.5f)
            {
                Vector2 axis = InputManager.Instance.Axis;
                transform.position += new Vector3(axis.x * _speed * Time.deltaTime * 60f, 0, _straigthSpeed * GameManager.Instance.LevelDifficult * Time.deltaTime * 60f);
            } else
            {
                CameraFollow.Instance.target = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject collisionGameObject = other.gameObject;
            string name = collisionGameObject.name;
            // Collision with PaintItem
            Collectable paintItem = collisionGameObject.GetComponent<Collectable>();
            if (paintItem != null)
            {
                PaintItemCollide(paintItem);
                return;
            }
            FinishBlock finishBlock = collisionGameObject.GetComponent<FinishBlock>();
            if (finishBlock != null)
            {
                CanShoot = true;
                Stop = true;
            }
        }

        private void UpdateShooting()
        {
            if (!CanShoot) return;
            _shootTimer -= Time.deltaTime * 60f;

            if (_shootTimer < 0)
            {
                _shootTimer = _reloadSpeed;

                Bullet bullet = Instantiate(Content.Bullet);
                bullet.transform.position = _aim.transform.position;
            }
        }

        private void PaintItemCollide(Collectable item)
        {
            PaintHolder.AddItem(item as PaintItem);
            item.Collect();
        }

        private void OnDrawGizmos()
        {
            // Set the Gizmo color to green
            Gizmos.color = Color.green;

            // Calculate cone properties
            Vector3 topPosition = transform.position + (transform.up * coneHeight);
            float angleIncrement = 360f / coneSegments;
            Quaternion coneRotation = Quaternion.LookRotation(transform.forward, transform.up);

            // Create the vertices of the cone base
            Vector3[] vertices = new Vector3[coneSegments + 1];
            for (int i = 0; i <= coneSegments; i++)
            {
                float angle = i * angleIncrement;
                Quaternion rotation = Quaternion.Euler(0f, angle, 0f) * coneRotation;
                Vector3 vertexPosition = transform.position + (rotation * (Vector3.forward * coneRadius));
                vertices[i] = vertexPosition;
            }

            // Draw the cone mesh
            for (int i = 0; i < coneSegments; i++)
            {
                Gizmos.DrawLine(vertices[i], vertices[i + 1]);
                Gizmos.DrawLine(vertices[i], topPosition);
            }

            Gizmos.DrawLine(vertices[coneSegments], vertices[0]);
            Gizmos.DrawLine(vertices[coneSegments], topPosition);
        }

        private Vector3 RandomPointInCone(GameObject gameObject)
        {
            // Calculate a random point within the cone
            float radius = Random.Range(0f, coneRadius);
            float angle = Random.Range(0f, 360f);
            Vector3 position = gameObject.transform.position + (Quaternion.Euler(0f, angle, 0f) * (Vector3.forward * radius));
            return position;
        }
    }
}