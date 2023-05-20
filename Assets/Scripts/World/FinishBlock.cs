using PaintRush;
using PaintRush.Controller;
using PaintRush.World;
using UnityEngine;

namespace World
{
    public class FinishBlock : Block
    {
        [SerializeField]
        private Vector3 _movePos;
        private Renderer _renderer;

        public Renderer Renderer
        {
            get { return _renderer; }
            set
            {
                _renderer = value;
            }
        }

        private float _shaderFill = 1.0f;
        public float ShaderFill
        {
            get { return _shaderFill; }
            set
            {
                _shaderFill = Mathf.Clamp(value, 0, 1);
                print(_renderer.material.GetFloat("_FillPercentage"));
                _renderer.material.SetFloat("_FillPercentage", _shaderFill);
                if (Mathf.Approximately(_shaderFill, 0f))
                {
                    opened = true;
                    PlayerController.Instance.Stop = false;
                }
            }
        }

        private bool opened = false;
        private void Awake()
        {
            _renderer = transform.Find("Plane").GetComponent<Renderer>();
        }

        private void Update()
        {
            if (opened) {
                _renderer.transform.localPosition = Vector3.MoveTowards(_renderer.transform.localPosition, _movePos, 1f * Time.deltaTime * 60f);
            }
        }
    }
}