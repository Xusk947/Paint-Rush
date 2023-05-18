using PaintRush;
using PaintRush.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace PaintRush.Painting
{

    public class PaintCanvas : MonoBehaviour
    {
        public delegate void OnPaintFinish(bool finished);

        public static event OnPaintFinish PaintEvent;

        /***
         * When Function Paint called after this value it will take a 1 point from ScoreBoard
         */
        public int CountUseModifier = 3;
        public bool Finished;
        [SerializeField, ReadOnly(true)]
        private Texture2D _texture, _currentTexture;
    
        private Renderer _renderer;
        private Material _material;
        private Dictionary<Color, List<Vector2Int>> _pixels;

        private float _pixelFillTime = 10f;

        public List<Color> Colors
        {
            get
            {
                return _pixels.Keys.ToList();
            }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                _currentTexture = new Texture2D(_texture.width, _texture.height, _texture.format, _texture.mipmapCount > 1);
                _currentTexture.filterMode = _texture.filterMode;

                _material.mainTexture = _currentTexture;

                transform.localScale = new Vector3(1, 1, 1);

                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                _pixels = new Dictionary<Color, List<Vector2Int>>();

                if (meshRenderer != null && meshRenderer.sharedMaterial != null)
                {
                    float ration = (_texture.width > _texture.height ?  (float) _texture.height / _texture.width : (float) _texture.width / _texture.height);
                    print(_texture.width + "w : " + _texture.height + "h | ration: " + ration);
                    transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.x * ration);
                    for (int x = 0; x < _texture.width; x++)
                    {
                        for (int y = 0; y < _texture.height; y++)
                        {
                            Color newColor = _texture.GetPixel(x, y);
                            if (_pixels.ContainsKey(newColor))
                            {
                                _pixels[newColor].Add(new Vector2Int(x, y));
                                continue;
                            }
                            _pixels.Add(newColor, new List<Vector2Int>());
                        }
                    }
                }

            }
        }

        public Texture2D CurrentTexture
        {
            get { return _currentTexture; }
            set 
            { 
                _currentTexture = value;
                _material.mainTexture = _currentTexture;
            }
        }

        public Dictionary<Color, List<Vector2Int>> Pixels
        {
            get { return _pixels; }
            set 
            {
                var keys = _pixels.Keys.ToList();
                var values = _pixels.Values.ToList();
                for(int i = 0; i < _pixels.Count; i++)
                {
                    var key = keys[i];
                    for(int k = 0; k < values[i].Count; k++)
                    {
                        var pixel = values[i][k];
                        if (!value[key].Contains(pixel))
                        {
                            _currentTexture.SetPixel(pixel.x, pixel.y, key);
                        }
                    }
                }
                _currentTexture.Apply();
                _pixels = value;
            }
        }


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = _renderer.sharedMaterial;
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
        }
        public IEnumerator FillPixels(PlayerController player)
        {
            List<Color> colors = _pixels.Keys.ToList();

            int countUse = CountUseModifier;
            PaintScoreText.Instance.Score = player.CollectedBalls;

            for (int i = 0; i < colors.Count; i++)
            {
                Color value = colors[i];
                List<Vector2Int> colorPixels = _pixels[value];
                while (player.CollectedBalls > 0)
                {

                    if (colorPixels.Count <= 0) break;
                    Vector2Int pixel = colorPixels[0];

                    _currentTexture.SetPixel(pixel.x, pixel.y, value);

                    colorPixels.Remove(pixel);
                    countUse--;
                    if (countUse < 0)
                    {
                        countUse = CountUseModifier;
                        player.PaintHolder.RemoveItem();
                        _currentTexture.Apply();
                    }

                    _pixelFillTime -= 0.1f;
                    PaintScoreText.Instance.Score = player.CollectedBalls;

                    yield return WaitForSecondsMillisec(_pixelFillTime);
                }
            }

            bool finished = true;

            for (int i = 0; i < colors.Count; i++)
            {
                Color value = colors[i];
                if (_pixels[value].Count > 0) finished = false;
            }

            PaintEvent?.Invoke(finished);
            Finished = finished;
            _pixelFillTime = 10f;
        }

        private IEnumerator WaitForSecondsMillisec(float milliseconds)
        {
            float startTime = Time.realtimeSinceStartup;
            float endTime = startTime + milliseconds / 1000f;

            while (Time.realtimeSinceStartup < endTime)
            {
                yield return null;
            }
        }
    }
}
