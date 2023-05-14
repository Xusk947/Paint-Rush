﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private float _straigthSpeed = 1f;
    [SerializeField]
    private float _speed = 1f;

    private bool _moveTowardsFinish;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (_moveTowardsFinish) UpdateMovementToFinish();
        else UpdateMovement();
        //_characterController.Move(new Vector3(axis.x * _speed, 0, _straigth_speed * GameManager.Instance.LevelDifficult));
    }

    private void UpdateMovement()
    {
        if (transform.position.y > -0.5f)
        {
            Vector2 axis = InputManager.Instance.Axis;
            transform.position += new Vector3(axis.x * _speed * Time.deltaTime * 60f, 0, _straigthSpeed * GameManager.Instance.LevelDifficult * Time.deltaTime * 60f);
        } else
        {
            CameraFollow.Instance.target = null;
        }
    }

    private void UpdateMovementToFinish()
    {
        transform.position = Vector3.MoveTowards(transform.position, FinishBlock.Instance.PlayerPosition.transform.position, _speed * Time.deltaTime * 60f);
        CameraFollow.Instance.locationOffset = Vector3.MoveTowards(CameraFollow.Instance.locationOffset, new Vector3(0, 9f, -11f), Time.deltaTime * 60f);
        CameraFollow.Instance.rotationOffset = Vector3.MoveTowards(CameraFollow.Instance.rotationOffset, new Vector3(15f, 0, 0), Time.deltaTime * 60f);
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionGameObject = other.gameObject;
        // Collision with PaintItem
        Collectable paintItem = collisionGameObject.GetComponent<Collectable>();
        if (paintItem != null)
        {
            PaintItemCollide(paintItem);
            return;
        }
        print("Check for another");
        FinishBlock finishBlock = collisionGameObject.GetComponent<FinishBlock>();
        if (finishBlock != null)
        {
            print("its a finish block");
            _moveTowardsFinish = true;
    }
    }

    private void PaintItemCollide(Collectable item)
    {
        item.Collect();
    }
}