using System;
using Basic.Attacks;
using Basic.Movements;
using Interfaces;
using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(AttackMovement))]
    [RequireComponent(typeof(TriggerAttack))]
    public class Bullet : MonoBehaviour, IDeleteAble
    {
        public event Action<GameObject> DeleteGameObjectEvent;

        private AttackMovement _movement;
        private TriggerAttack _attack;

        private void Start()
        {
            _attack = GetComponent<TriggerAttack>();
            _movement = GetComponent<AttackMovement>();
            _movement.Move(transform.up, ForceMode2D.Impulse);
            _attack.HitEvent += Hit;
        }

        private void Hit(GameObject hitGameObject)
        {
            Delete();
        }
        
        public void Delete()
        {
            Destroy(gameObject);
            DeleteGameObjectEvent?.Invoke(gameObject);
        }

    }
}