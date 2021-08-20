using System;
using ArtificialIntelligences;
using Basic.Attacks;
using Basic.Attacks.Info;
using Basic.Healths;
using Basic.Inputs;
using Basic.Movements;
using Global;
using Interfaces;
using UnityEngine;

namespace Basic
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(ICustomInput))]
    [RequireComponent(typeof(BulletAttack))]
    [RequireComponent(typeof(LaserAttack))]
    [RequireComponent(typeof(CollisionAttack))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour, ITakeDamage, IChange<Health>, IDeleteAble, IHitCallback
    {
        [SerializeField] private float _bodyDamage;
        [SerializeField] private float _attackDamage;
        [SerializeField] private float _ultimateDamage;
        [SerializeField] private PlayerAI _ai;
        
        public event Action<GameObject> DeleteGameObjectEvent;

        private PlayerMovement _movement;
        private ICustomInput _input;
        private BulletAttack _bulletAttack;
        private LaserAttack _laserAttack;
        private CollisionAttack _collisionAttack;
        private Health _health;
        
        public event Action<Health> ChangeEvent;
        public static Player Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
            
            _movement = GetComponent<PlayerMovement>();
            _input = GetComponent<ICustomInput>();
            _bulletAttack = GetComponent<BulletAttack>();
            _laserAttack = GetComponent<LaserAttack>();
            _health = GetComponent<Health>();
            _collisionAttack = GetComponent<CollisionAttack>();
            
            _collisionAttack._info = new AttackInfo(gameObject)
            {
                Damage = _bodyDamage
            };
        }


        private void FixedUpdate()
        {
            Attack();
            Ultimate();
            Move();
        }

        private  void Attack()
        {
            if (!_input.IsAttackButton() || _bulletAttack.IsReload()) return;
            _bulletAttack.Fire(new AttackInfo(gameObject)
            {
                Damage = _attackDamage
            });
            GameScore.Instance.AttackCalculate();
            _bulletAttack.Reload();
        }

        private  void Ultimate()
        {
            if (!_input.IsUltimateButton() || _laserAttack.IsReload()) return;
            _laserAttack.Fire(new AttackInfo(gameObject)
            {
                Damage = _ultimateDamage
            });
            GameScore.Instance.UltimateCalculate();
            _laserAttack.Reload();
        }

        private  void Move()
        {
            if (_input.IsMove())
            {
                _movement.Move(transform.up);
            }

            if (_input.IsLeftRotationButton())
            {
                _movement.Rotate(Vector3.forward);
            }else if (_input.IsRightRotationButton())
            {
                _movement.Rotate(Vector3.back);
            }
            _movement.Limit(1);
        }

        public void TakeDamage(AttackInfo info)
        {
            if (info.IsOwner(gameObject)) return;
            _health.Hurt(info.Damage);
            if (_health.IsDeath())
            {
                Delete();
            }
            ChangeEvent?.Invoke(_health);
        }
        
        public void Delete()
        {
            DeleteGameObjectEvent?.Invoke(gameObject);
            _ai?.PlayerDeath();
            App.EndGame();
            Restart();
        }

        private void Restart()
        {
            _movement.Limit(0);
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            _health.Heal();
        }

        public void HitCallback(HitInfo hitInfo)
        {
            GameScore.Instance.HitCalculate(hitInfo);
        }

        public void SetReward(float value)
        {
            _ai?.SetReward(value);
        }
    }
}
