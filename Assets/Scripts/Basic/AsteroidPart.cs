using System;
using Basic.Attacks;
using Basic.Healths;
using Basic.Movements;
using Interfaces;
using Other;
using UnityEngine;

namespace Basic
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CollisionAttack))]
    [RequireComponent(typeof(ExplosionForce2D))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]

    public class AsteroidPart : MonoBehaviour, IDeleteAble, ITakeDamage
    {
        [SerializeField] private float _damage;
        [SerializeField] private AudioClip _destroyAudioClip;
 
        public event Action<GameObject> DeleteGameObjectEvent;

        private Health _health;
        private CollisionAttack _collisionAttack;
        private ExplosionForce2D _explosionForce2D;
        private Animator _animator;
        private Collider2D _collider;
        private Movement _movement;
        private static readonly int Destroy1 = Animator.StringToHash("Destroy");

        private void Awake()
        {
            _explosionForce2D = GetComponent<ExplosionForce2D>();
            _health = GetComponent<Health>();
            _movement = GetComponent<Movement>();
            _collisionAttack = GetComponent<CollisionAttack>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            _collisionAttack._info = new AttackInfo
            {
                Damage = _damage,
                Owner = gameObject
            };
        }

        private void Start()
        {
            _movement.Move(DirectionToPlayer(), ForceMode2D.Impulse);
        }

        public void Delete()
        {
            _movement.Limit(0);
            _collider.enabled = false;
            _animator.SetTrigger(Destroy1);
            _explosionForce2D.Explosion2D(transform.position);
            MainCamera.Instance.Audio().PlayOneShot(_destroyAudioClip);
            Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
            DeleteGameObjectEvent?.Invoke(gameObject);
        }


        public void TakeDamage(AttackInfo info)
        {
            var healthAfterAttack = _health.Hurt(info.Damage);
            if ( healthAfterAttack == 0)
            {
                Delete();
            }
        }
        private Vector3 DirectionToPlayer()
        {
            var player = Player.Instance;
            var distance = player.transform.position - transform.position;
            return distance.normalized;
        }

    }
}