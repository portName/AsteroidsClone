using System;
using System.Collections;
using System.Collections.Generic;
using Basic.Inputs;
using Interfaces;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace ArtificialIntelligences
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAI : AI, ICustomInput
    {

        
        
        [SerializeField]private float _sensorUpdateTime;
        [SerializeField]private float _searchEnemyRadius;

        private float _isLeft;
        private float _isRight;
        private float _isAttack;
        private float _isMove;
        private float _isUltimate;
        
        private List<Vector3> _distanceToAsteroids = new List<Vector3>();
        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartCoroutine(SensorUpdate());
        }

        private IEnumerator SensorUpdate()
        {
            while (true)
            {
                SearchEnemy();
                yield return new WaitForSeconds(_sensorUpdateTime);
            }
        }

        private void SearchEnemy()
        {
            _distanceToAsteroids.Clear();
            var listResult = new List<Collider2D>();

            Physics2D.OverlapCircle(transform.position, _searchEnemyRadius, new ContactFilter2D(), listResult);
            foreach (var hitColliders2D in listResult)
            {
                _distanceToAsteroids.Add(hitColliders2D.transform.position - transform.position);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchEnemyRadius);
            foreach (var distance in _distanceToAsteroids)
            {
                var position = transform.position;
                Debug.DrawLine(position, position + distance, Color.cyan);
            }
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            // Target and Agent positions
            sensor.AddObservation(transform.position);
            foreach (var distance in _distanceToAsteroids)
            {
                sensor.AddObservation(distance);
            }
            sensor.AddObservation(_rigidbody2D.velocity);
            sensor.AddObservation(transform.rotation);
        }

        public override void OnActionReceived(float[] vectorAction)
        {
            _isLeft = vectorAction[0];
            _isRight = vectorAction[1];
            _isAttack = vectorAction[2];
            _isMove = vectorAction[3];
            _isUltimate = vectorAction[4];
        }

        public bool IsAttackButton()
        {
            return _isAttack > 0.1;
        }

        public bool IsUltimateButton()
        {
            return _isUltimate > 0.1;
        }

        public bool IsLeftRotationButton()
        {
            return _isLeft > 0.1;
        }

        public bool IsRightRotationButton()
        {
            return _isRight > 0.1;
        }

        public bool IsMove()
        {
            return _isMove > 0.1;
        }

        public Vector3 Move()
        {
            return Vector3.up;
        }
    }
}