using UnityEngine;

namespace Basic.Movements
{
    [RequireComponent(typeof(Rigidbody2D))]
    [DisallowMultipleComponent]
    public class AsteroidMovement : Movement
    {
        [SerializeField] private float _speedMove;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.freezeRotation = true;
        }

        public override void Move(Vector3 move)
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Vector3 move, ForceMode2D forceMode2D)
        {
            _rigidbody2D.AddForce(move * _speedMove, forceMode2D);
        }

        public override void Rotate(Vector3 rotate)
        {
            throw new System.NotImplementedException();
        }

        public override void Limit(float limitVelocityMagnitude)
        {
            _rigidbody2D.velocity = Vector3.ClampMagnitude(_rigidbody2D.velocity, limitVelocityMagnitude);
        }
    }
}