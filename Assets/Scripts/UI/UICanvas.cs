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

            // Get references to the buttons and add click listeners
            _shop = transform.Find("ShopButton").GetComponent<Button>();
            _shop.onClick.AddListener(OnShopButtonClick);

            _pressToPlay = transform.Find("PressToPlayButton").GetComponent<Button>();
            _pressToPlay.onClick.AddListener(OnPressToPlayClick);

            // Get reference to the shop UI panel
            _shopUI = transform.Find("ShopUI").GetComponent<RectTransform>();

            // Get reference to the return button in the shop UI panel and add click listener
            _returnButton = _shopUI.Find("ReturnButton").GetComponent<Button>();
            _returnButton.onClick.AddListener(OnReturnButtonClick);
        }

        private void OnShopButtonClick()
        {
            // Hide the press to play button, shop button, and show the shop UI panel
            _pressToPlay.gameObject.SetActive(false);
            _shop.gameObject.SetActive(false);
            _shopUI.gameObject.SetActive(true);
        }

        private void OnPressToPlayClick()
        {
            // Resume the game, hide the UI canvas, and show the game canvas
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            GameCanvas.Instance.gameObject.SetActive(true);
            PaintScoreText.Instance.gameObject.SetActive(true);
        }

        private void OnReturnButtonClick()
        {
            // Show the press to play button, shop button, and hide the shop UI panel
            _pressToPlay.gameObject.SetActive(true);
            _shop.gameObject.SetActive(true);
            _shopUI.gameObject.SetActive(false);
        }
    }
}