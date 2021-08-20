using System;
using System.Collections;
using UnityEngine;

namespace Global
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float _timerUpdate;

        private float _value;
        private Coroutine _timer;
        public float Value
        {
            get => _value;
            private set
            {
                _value = value;
                ChangeTimeEvent?.Invoke(_value);
            }
        }
        public static GameTimer Instance { get; private set; }
        public event Action<float> ChangeTimeEvent;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            App.EndGameEvent += Restart;
        }

        private void Start()
        {
            _timer = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timerUpdate);
                Value += _timerUpdate;
            }
        }


        private void Restart()
        {
            StopCoroutine(_timer);
            _value = 0;
            _timer = StartCoroutine(Timer());
        }
        private void OnDestroy()
        {
            App.EndGameEvent -= Restart;
        }
    }
}