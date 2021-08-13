using System;
using Interfaces;
using UnityEngine;

namespace Basic.Healths
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        public float Value() => _health;
        public float Heal() => _health = _maxHealth;
        


        public float Hurt(float value)
        {
            var remainder = _health - value;
            if (remainder >= 0)
            {
                _health -= value;
            }
            else
            {
                _health = 0;
            }
            return _health;
        }

    }
}