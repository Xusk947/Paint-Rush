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

        private Image _continueButton;
        private Image _adButton;

        private void Awake()
        {
            Instance = this;

            // Find and assign references to the Continue and AD buttons
            _continueButton = transform.Find("Continue").GetComponent<Image>();
            _adButton = transform.Find("AD").GetComponent<Image>();
        }

        private void Start()
        {
            // Initialization code for the GameCanvas
        }

        /**
         * Toggle the visibility of the Continue and AD buttons
         */
        public void ToggleTitles(bool visibility)
        {
            _continueButton.gameObject.SetActive(visibility);
            _adButton.gameObject.SetActive(visibility);
        }

        public void OnContinueClick()
        {
            // Handle the Continue button click event
            SceneManager.LoadScene(0);
        }

        public void OnAdClick()
        {
            // Handle the AD button click event
            // TODO: Implement AD logic
        }
    }
}