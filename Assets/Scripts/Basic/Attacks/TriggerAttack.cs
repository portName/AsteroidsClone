using System;
using Basic.Attacks.Info;
using Interfaces;
using UnityEngine;

namespace Basic.Attacks
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerAttack : MonoBehaviour, IHitAttack
    {
        public AttackInfo _info { get; set; }
        
        public event Action<GameObject> HitEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var takeDamage = other.gameObject.GetComponent<ITakeDamage>();
            if (takeDamage == null) return;
            takeDamage.TakeDamage(_info);
            HitEvent?.Invoke(other.gameObject);
        }

    }
}