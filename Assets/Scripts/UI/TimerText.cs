using System.Globalization;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class TimerText : MonoBehaviour
    {
        private Text _textUI;
        private GameTimer _timer;

        private void Start()
        {
            _textUI = GetComponent<Text>();
            _timer = GameTimer.Instance;
            ShowTimer(_timer.Value);
            _timer.ChangeTimeEvent += ChangeTimeHandler;
        }

        private void ChangeTimeHandler(float timer)
        {
            ShowTimer(timer);
        }

        private void ShowTimer(float timer)
        {
            _textUI.text = "Time: " + timer.ToString(CultureInfo.InvariantCulture);
        }
        
        private void OnDestroy()
        {
            _timer.ChangeTimeEvent -= ChangeTimeHandler;
        }
    }
}