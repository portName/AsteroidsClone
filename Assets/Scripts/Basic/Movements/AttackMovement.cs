using System;
using UnityEngine;

namespace Basic.Movements
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackMovement : Movement
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.freezeRotation = true;
        }

        public override void Move(Vector3 move)
        {
            _rigidbody2D.AddForce(move * _speed);
        }
        public override void Move(Vector3 move, ForceMode2D forceMode2D)
        {
            _rigidbody2D.AddForce(move * _speed, forceMode2D);
        }

        public override void Rotate(Vector3 rotate)
        {
            throw new System.NotImplementedException();
        }

        public override void Limit(float limitVelocityMagnitude)
        {
            throw new System.NotImplementedException();
        }
    }
}