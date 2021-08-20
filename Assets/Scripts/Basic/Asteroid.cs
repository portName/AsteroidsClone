using System;
using Basic.Attacks;
using Basic.Attacks.Info;
using Basic.Healths;
using Basic.Movements;
using Interfaces;
using Other;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Basic
{
    #region RequireComponents

    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(CollisionAttack))]
    [RequireComponent(typeof(ExplosionForce2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Renderer))]

    #endregion
    public class Asteroid : MonoBehaviour, IDeleteAble, ITakeDamage
    {
        #region Private Serialize Filed

        [SerializeField] private float _damage;
        [SerializeField] private AudioClip _destroyAudioClip;

        #endregion

        #region Events

        public event Action<GameObject> DeleteGameObjectEvent;

        #endregion

        #region Private Fields

        private Movement _movement;
        private Health _health;
        private CollisionAttack _collisionAttack;
        private ExplosionForce2D _explosionForce2D;
        private Animator _animator;
        private Collider2D _collider;

        #endregion

        #region Animator Hash

        private static readonly int Destroy1 = Animator.StringToHash("Destroy");

        #endregion

        private void Start()
        {
            _movement = GetComponent<AsteroidMovement>();
            _explosionForce2D = GetComponent<ExplosionForce2D>();
            _health = GetComponent<Health>();
            _collisionAttack = GetComponent<CollisionAttack>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            
            _collisionAttack._info = new AttackInfo(gameObject)
            {
                Damage = _damage
            };
            
            _movement.Move(DirectionToPlayer(), ForceMode2D.Impulse);

        }
        
        public void Delete()
        {
            if (GetComponent<Renderer>().isVisible)
            {
                _movement.Limit(0);
                _collider.enabled = false;
                _animator.SetTrigger(Destroy1);
                _explosionForce2D.Explosion2D(transform.position);
                MainCamera.Instance.Audio().PlayOneShot(_destroyAudioClip);
                Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                Destroy(gameObject);
            }
            DeleteGameObjectEvent?.Invoke(gameObject);
        }


        public void TakeDamage(AttackInfo info)
        {
            var hitInfo = new HitInfo
            {
                remainingHealth = _health.Hurt(info.Damage),
                isDeath = _health.IsDeath(),
                distance = info.DistanceFromHitObject(transform)
            };
            if (hitInfo.isDeath)
            {
                Delete();
            }
            info.Owner.GetComponent<IHitCallback>()?.HitCallback(hitInfo);
        }

        private Vector3 DirectionToPlayer()
        {
            var player = Player.Instance;
            var distance = player.transform.position  - transform.position;
            return distance.normalized;
        }
    }
}