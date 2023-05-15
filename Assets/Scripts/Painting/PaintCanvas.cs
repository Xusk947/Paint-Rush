using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaintCanvas : MonoBehaviour
{
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
                transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.x + (float)_texture.width / _texture.height);
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


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.sharedMaterial;
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }
    public IEnumerator FillPixels(int count)
    {
        List<Color> colors = _pixels.Keys.ToList();

        int countUseModifier = 3;
        int countUse = countUseModifier;
        PaintScoreText.Instance.Score = count;

        for (int i = 0; i < colors.Count; i++)
        {
            Color value = colors[i];
            List<Vector2Int> colorPixels = _pixels[value];
            while (count > 0)
            {

                if (colorPixels.Count <= 0) break;
                Vector2Int pixel = colorPixels[0];

                _currentTexture.SetPixel(pixel.x, pixel.y, value);
                _currentTexture.Apply();

                colorPixels.Remove(pixel);
                countUse--;
                if (countUse < 0)
                {
                    countUse = countUseModifier;
                    count--;
                }

                _pixelFillTime -= 0.1f;
                PaintScoreText.Instance.Score = count;

                yield return WaitForSecondsMillisec(_pixelFillTime);
            }
        }

        bool finished = true;

        for (int i = 0; i < colors.Count; i++)
        {
            Color value = colors[i];
            if (_pixels[value].Count > 0) finished = false;
        }

        print(finished);

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
