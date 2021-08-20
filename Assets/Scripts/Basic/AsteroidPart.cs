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
    #region Require Components

    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CollisionAttack))]
    [RequireComponent(typeof(ExplosionForce2D))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Renderer))]

    #endregion

    public class AsteroidPart : MonoBehaviour, IDeleteAble, ITakeDamage
    {
        #region Private Serialize Field

        [SerializeField] private float _damage;
        [SerializeField] private AudioClip _destroyAudioClip;

        #endregion

        #region Events

        public event Action<GameObject> DeleteGameObjectEvent;

        #endregion

        #region Private Fields

        private Health _health;
        private CollisionAttack _collisionAttack;
        private ExplosionForce2D _explosionForce2D;
        private Animator _animator;
        private Collider2D _collider;
        private Movement _movement;

        #endregion

        #region Animator Hash

        private static readonly int Destroy1 = Animator.StringToHash("Destroy");

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {

            #region InitComponents

            _explosionForce2D = GetComponent<ExplosionForce2D>();
            _health = GetComponent<Health>();
            _movement = GetComponent<Movement>();
            _collisionAttack = GetComponent<CollisionAttack>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();

            #endregion

            #region InitComponentsProps

            _collisionAttack._info = new AttackInfo(gameObject)
            {
                Damage = _damage
            };

            #endregion
            
        }

        private void Start()
        {
            _movement.Move(DirectionToPlayer(), ForceMode2D.Impulse);
        }

        #endregion

        #region Interface DeleteAble

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

        #endregion

        #region Interface TakeDamage

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

        #endregion

        #region Custom Methods

        private Vector3 DirectionToPlayer()
        {
            var player = Player.Instance;
            var distance = player.transform.position - transform.position;
            return distance.normalized;
        }

        #endregion

    }
}