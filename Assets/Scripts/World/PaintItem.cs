using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintItem : Collectable
{
    [SerializeField]
    private int value = 1;
    private void Start()
    {
        ChangeColor(GameManager.Instance.PaintCanvas.Colors);
    }

    public override void Collect()
    {
        if (_colected) return;
        base.Collect();
        PaintScoreText.Instance.AddScore(value);
    }

    protected override void ChangeColor(List<Color> colors)
    {
        base.ChangeColor(colors);
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
            childRenderer.material.color = colors[Random.Range(0, colors.Count)];
        }
    }

    protected override void SubtractColor(Color color)
    {
        base.SubtractColor(color);
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
            childRenderer.material.color -= color;
        }
    }
}