using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private float _straigthSpeed = 1f;
    [SerializeField]
    private float _speed = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (transform.position.y > -0.5f)
        {
            Vector2 axis = InputManager.Instance.Axis;
            transform.position += new Vector3(axis.x * _speed * Time.deltaTime * 60f, 0, _straigthSpeed * GameManager.Instance.LevelDifficult * Time.deltaTime * 60f);
        } else
        {
            CameraFollow.Instance.target = null;
        }
        //_characterController.Move(new Vector3(axis.x * _speed, 0, _straigth_speed * GameManager.Instance.LevelDifficult));
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionGameObject = other.gameObject;
        // Collision with PaintItem
        Collectable paintItem = collisionGameObject.GetComponent<Collectable>();
        if (paintItem != null) PaintItemCollide(paintItem);
    }

    private void PaintItemCollide(Collectable item)
    {
        item.Collect();
    }
}