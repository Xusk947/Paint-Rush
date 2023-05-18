using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintRush.UI
{

    public class UICanvas : MonoBehaviour
    {
        public static UICanvas Instance;

        private Button _shop, _pressToPlay, _returnButton;
        private RectTransform _shopUI;

        private void Awake()
        {
            Time.timeScale = 0f;
            Instance = this;

            _shop = transform.Find("ShopButton").GetComponent<Button>();
            _shop.onClick.AddListener(OnShopButtonClick);

            _pressToPlay = transform.Find("PressToPlayButton").GetComponent<Button>();
            _pressToPlay.onClick.AddListener(OnPressToPlayClick);

            _shopUI = transform.Find("ShopUI").GetComponent<RectTransform>();

            _returnButton = _shopUI.Find("ReturnButton").GetComponent<Button>();
            _returnButton.onClick.AddListener(OnReturnButtonClick);
        }

        private void OnShopButtonClick()
        {
            _pressToPlay.gameObject.SetActive(false);
            _shop.gameObject.SetActive(false);
            _shopUI.gameObject.SetActive(true);
        }

        private void OnPressToPlayClick()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            GameCanvas.Instance.gameObject.SetActive(true);
        }

        private void OnReturnButtonClick()
        {
            _pressToPlay.gameObject.SetActive(true);
            _shop.gameObject.SetActive(true);
            _shopUI.gameObject.SetActive(false);
        }
    }
}
