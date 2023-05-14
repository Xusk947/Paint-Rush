using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private float _levelDifficult = 1.0f;

    public PaintCanvas PaintCanvas;

    [SerializeField]
    public float LevelDifficult
    {
        get { return _levelDifficult; }
    }

    private void Start()
    {
        PaintCanvas = Instantiate(Resources.Load<PaintCanvas>("Prefabs/PaintItem"));
        PaintCanvas.Texture = Resources.Load<Texture2D>("Images/cat");
    }

    private void Update()
    {
        _levelDifficult += 0.00003f;
    }
    private void Awake()
    {
        Instance = this;
    }
}