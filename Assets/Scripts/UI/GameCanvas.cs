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
        }

        /***
         * Show AD, Continue buttons
         */
        public void ToggleTitles(bool visibility)
        {
            _continue.gameObject.SetActive(visibility);
            _ad.gameObject.SetActive(visibility);
        }

        public void OnContinueClick()
        {
            SceneManager.LoadScene(0);
        }

        public void OnAdClick()
        {
            // TODO ADD AD xD
        }
    }
}
