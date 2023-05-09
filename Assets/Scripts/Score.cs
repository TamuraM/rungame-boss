using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] private Text _scoreText = default;

    private void Start()
    {
        SetScoreText(_score);
    }

    public void AddScore(int addValue)
    {
        _score += addValue;
        SetScoreText(_score);
    }

    private void SetScoreText(int score)
    {
        string text = score.ToString("00000");
        _scoreText.text = text;
    }

}
