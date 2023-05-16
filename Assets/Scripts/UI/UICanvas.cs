using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintRush.UI
{

    public class UICanvas : MonoBehaviour
    {
        public static UICanvas Instance;

        private Image _shop;
        private Image _pressToPlay;

        private void Awake()
        {
            Time.timeScale = 0f;
            Instance = this;

            _shop = transform.Find("Shop").GetComponent<Image>();
            _pressToPlay = transform.Find("PressToPlay").GetComponent<Image>();
        }

        public void ShopButtonClick()
        {

        }

        public void PressToPlayClick()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            GameCanvas.Instance.gameObject.SetActive(true);
        }
    }
}
