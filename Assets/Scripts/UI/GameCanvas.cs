using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance;

    private Image _continue, _ad;
    private void Awake()
    {
        Instance = this;
        _continue = transform.Find("Continue").GetComponent<Image>();
        _ad = transform.Find("AD").GetComponent<Image>();
    }

    private void Start()
    {
        PaintCanvas.PaintEvent += PaintFinished;
    }

    private void PaintFinished(bool finished)
    {
        ToggleTitles(true);
    }

    /***
     * Show AD, Continue buttons
     */
    public void ToggleTitles(bool visibility)
    {
        _continue.gameObject.SetActive(visibility);
        _ad.gameObject.SetActive(visibility);
    }

    private void OnDestroy()
    {
        PaintCanvas.PaintEvent -= PaintFinished;
    }

    public void OnContinueClick()
    {
        GameData gd = GameData.Instance;

        if (gd == null)
        {
            gd = new GameData();
        }

        gd.ImportPaintCanvasData(GameManager.Instance.PaintCanvas);

        SceneManager.LoadScene(0);
    }

    public void OnAdClick()
    {
        // TODO ADD AD xD
    }
}
