using System.Collections;
using UnityEngine;

public class PaintItem : Collectable
{
    [SerializeField]
    private int value = 1;

    private void Start()
    {
        ChangeColor(GameManager.Instance.CollectableColor);
    }

    public override void Collect()
    {
        if (_colected) return;
        base.Collect();
        PaintScoreText.Instance.AddScore(value);
    }

    protected override void ChangeColor(Color color)
    {
        base.ChangeColor(color);
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
            childRenderer.material.color = color;
        }
    }
}