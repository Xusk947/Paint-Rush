using PaintRush.Painting;
using PaintRush.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PaintRush.UI
{

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
            PaintCanvas paintCanvas = GameManager.Instance.PaintCanvas;
            print(XData.Instance);
            print(paintCanvas.Texture.name);
            if (XData.Instance.Textures.ContainsKey(paintCanvas.Texture.name))
            {
                print("Override data texture");
                XData.Instance.Textures[paintCanvas.Texture.name] = new TextureData(paintCanvas.Texture.name, true);
            }
            else
            {
                print("Create a new Texture data");
                XData.Instance.Textures.Add(paintCanvas.Texture.name, new TextureData(paintCanvas.Texture.name, true));
            }
            XData.Instance.Current = null;
            DataManager.SaveGame(XData.Instance);
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
}
