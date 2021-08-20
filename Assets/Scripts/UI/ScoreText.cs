using System;
using System.Globalization;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class ScoreText : MonoBehaviour
    {
        private Text _textUI;
        private GameScore _gameScore;

        private void Start()
        {
            _textUI = GetComponent<Text>();
            _gameScore = GameScore.Instance;
            _gameScore.ChangeScoreEvent += ChangeScoreHandler;
            ShowScore(_gameScore.Value);
        }

        private void ChangeScoreHandler(float score)
        {
            ShowScore(score);
        }

        private void ShowScore(float score)
        {
            _textUI.text = "Score: " + $"{score:0.00}";
        }
        
        private void OnDestroy()
        {
            _gameScore.ChangeScoreEvent -= ChangeScoreHandler;
        }
    }
}
