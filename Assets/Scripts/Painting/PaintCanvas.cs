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

    public void FillPixel(Color color)
    {
        if (!_pixels.ContainsKey(color)) return;
        // Get all points from Color Dictionary
        List<Vector2Int> _points = _pixels[color];
        
        if (_points.Count <= 0) return;

        /* TODO :: Finish fill 1 pixel and add FillPixels */    
        
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.sharedMaterial;
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }
}
