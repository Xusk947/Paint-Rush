using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                _text.text = "Score " + _score;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        _text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (_textScore != _score)
        {
            StartCoroutine(UpdateScore());
            _text.text = "Score " + _textScore;
        }
    }

    IEnumerator UpdateScore()
    {
        while (_textScore != _score)
        {
            _textScore = Mathf.Clamp(_textScore + 1, 0, _score);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void AddScore(int score)
    {
        _score += score;
    }
}
