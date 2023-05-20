using PaintRush.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.World
{

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
            PaintHolder paintHolder = PlayerController.Instance.PaintHolder;
            if (transform.position.z < paintHolder.transform.position.z )
            {
                transform.position = Vector3.MoveTowards(transform.position, paintHolder.transform.position, Time.deltaTime * 60f);
                if (Vector3.Distance(transform.position, paintHolder.transform.position) < 1f) gameObject.SetActive(false);
            } else
            {
                transform.position += new Vector3(0, .5f * Time.deltaTime * 60f, 0);
            }
        }

        protected virtual void ChangeColor(List<Color> colors)
        {
            _renderer.material.color = colors[Random.Range(0, colors.Count)] + new Color(0, 0, 0, 1f);
        }

        protected virtual void SubtractColor(Color color)
        {
            _renderer.material.color -= color;
        }

        public virtual void Collect() {
            _colected = true;
            _velocity = new Vector3(0, 0.1f, 0);
        }
    }
}
