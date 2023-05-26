using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintRush.UI
{
    public class PaintScoreText : MonoBehaviour
    {
        public static PaintScoreText Instance;

        private Text _text;
        private int _score = 0;
        private int _textScore = 0;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                if (_text != null)
                {
                    _text.text = _score.ToString();
                }
            }
        }

        private void Awake()
        {
            Instance = this;

            // Get the reference to the Text component
            _text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            // Update the score text if it's different from the current score
            if (_textScore != _score)
            {
                StartCoroutine(UpdateScore());
                _text.text = _textScore.ToString();
            }
        }

        IEnumerator UpdateScore()
        {
            // Gradually update the score text
            while (_textScore != _score)
            {
                _textScore = Mathf.Clamp(_textScore + 1, 0, _score);
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void AddScore(int score)
        {
            // Increase the score by the specified amount
            _score += score;
        }
    }
}