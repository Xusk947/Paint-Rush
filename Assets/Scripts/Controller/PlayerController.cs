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

        private bool _paintCalled = false;
        private float _reloadSpeed = .5f;
        private float _shootTimer = 0f;

        private GameObject _aim;
        
        public PaintHolder PaintHolder { get; private set; }
        public bool Stop = false;

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
                Stop = true;
            }
        }

        private void UpdateShooting()
        {
            _shootTimer -= Time.deltaTime;

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
    }
}