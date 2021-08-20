using System;
using System.Globalization;
using Basic;
using Basic.Attacks.Info;
using Interfaces;
using UnityEngine;

namespace Global
{
    public class GameScore : MonoBehaviour
    {
        #region Private Serialize Field

        [SerializeField] private float distanceWeight;
        [SerializeField] private float deathWeight;
        [SerializeField] private float hitWeight;
        [SerializeField] private float timeWeight;
        [SerializeField] private float attackWeight;
        [SerializeField] private float ultimateWeight;

        #endregion
        
        private float _value;
        private GameTimer _timer;
        public float Value
        {
            get => _value;
            private set
            {
                _value = value < 0? 0 : value;
                ChangeScoreEvent?.Invoke(_value);
            }
        }

        public event Action<float> ChangeScoreEvent;
        public static GameScore Instance { get; private set; }

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
            _timer = GameTimer.Instance;
            _timer.ChangeTimeEvent += TimerCalculate;
        }
        
    
        public void HitCalculate(HitInfo hitInfo)
        {
            var value = hitInfo.distance * distanceWeight +
                        hitInfo.IsDeath() * deathWeight +
                        hitWeight;
            Value += value;
            Player.Instance.SetReward(value);
        }
        public void TimerCalculate(float timer)
        {
            Value += timeWeight;
            Player.Instance.SetReward(timeWeight);

        }
        public void AttackCalculate()
        {
            Value += attackWeight;
            
            Player.Instance.SetReward(attackWeight);

        }
        public void UltimateCalculate()
        {
            Value += ultimateWeight;
            Player.Instance.SetReward(ultimateWeight);

        }
        private  void Restart()
        {
            Value = 0;
        }

        private void OnDestroy()
        {
            App.EndGameEvent -= Restart;
            _timer.ChangeTimeEvent -= TimerCalculate;
        }
    }
}
