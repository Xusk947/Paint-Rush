using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private Color _collectableColor;
    [SerializeField]
    private float _levelDifficult = 1.0f;

    [SerializeField]
    public float LevelDifficult
    {
        get { return _levelDifficult; }
    }

    private void Update()
    {
        _levelDifficult += 0.00003f;
    }
    public Color CollectableColor
    {
        get { return _collectableColor; }
    }

    private void Awake()
    {
        Instance = this;
    }
}