using PaintRush;
using PaintRush.Controller;
using PaintRush.World;
using UnityEngine;

namespace World
{
    public class FinishBlock : Block
    {

        private static int id = 0;

        [SerializeField]
        private Vector3 _movePos;
        private Renderer _renderer;
        public bool Filled { get; private set; }

        public Renderer Renderer
        {
            get { return _renderer; }
            set
            {
                _renderer = value;
            }
        }
        public float ShaderFill
        {
            get { return _renderer.material.GetFloat("_FillPercentage"); }
            set
            {
                float fillness = Mathf.Clamp(value, 0, 1);
                _renderer.material.SetFloat("_FillPercentage", fillness);
                if (Mathf.Approximately(fillness, 0f))
                {
                    Filled = true;
                    PlayerController.Instance.Stop = false;
                }
            }
        }

        private void Awake()
        {
            _renderer = transform.Find("Plane").GetComponent<Renderer>();
            _renderer.material = Instantiate(Content.FillerMaterial);
            _renderer.material.name = id.ToString();
            id++;
        }

        private void Update()
        {
            if (Filled) {
                _renderer.transform.localPosition = Vector3.MoveTowards(_renderer.transform.localPosition, _movePos, 1f * Time.deltaTime * 60f);
            }
        }
    }
}