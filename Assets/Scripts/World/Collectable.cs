using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    protected Renderer _renderer;

    protected bool _colected = false;
    private Vector3 _velocity; 
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if ( _colected )
        {
            CollectedAnimation();
        } else
        {
            transform.eulerAngles += new Vector3(0, 2.5f, 0);
        }
    }
    
    private void CollectedAnimation()
    {
        Color newColor = _renderer.material.color -= new Color(0, 0, 0, 0.1f);
        ChangeColor(newColor);
        transform.position += _velocity;

        if (_renderer.material.color.a < -1)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }
    public virtual void Collect() {
        _colected = true;
        _velocity = new Vector3(0, 0.1f, 0);
    }
}
