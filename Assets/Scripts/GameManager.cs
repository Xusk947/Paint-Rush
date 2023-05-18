using PaintRush.Painting;
using PaintRush.Data;
using System.Collections;
using UnityEngine;
using PaintRush.Input;
using System.Linq;

namespace PaintRush
{

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
            if (Application.isMobilePlatform) gameObject.AddComponent<MobileInputManager>();
            else gameObject.AddComponent<DesktopInputManager>();
            PaintCanvas = Instantiate(Resources.Load<PaintCanvas>("Prefabs/PaintItem"));

            if (GameData.Instance != null && !GameData.Instance.Finished)
            {
                GameData.Instance.ExportPaintCanvasData(PaintCanvas);
            } else
            {
                if (XData.Instance.Current != null)
                {
                    print("CURRENT TEXTURE LOADED");
                    PaintCanvas.Texture = Content.Textures[XData.Instance.Current.Name][0];
                    PaintCanvas.Pixels = XData.Instance.Current.Points;
                } else
                {
                    print("CURRENT TEXTURE IS NULL");
                    PaintCanvas.Texture = Content.Textures.First().Value[0];
                }
            }
        }

        private void Update()
        {
            _levelDifficult += 0.00003f;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnApplicationQuit()
        {
            if (!PaintCanvas.Finished)
            {
                XData.Instance.Current = new TextureData(PaintCanvas.Texture.name, PaintCanvas.Pixels);
            }
            DataManager.SaveGame(XData.Instance);
        }
    }
}