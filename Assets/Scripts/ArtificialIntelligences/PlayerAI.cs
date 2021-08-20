using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Global;
using Interfaces;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace ArtificialIntelligences
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAI : AI, ICustomInput
    {
        
        [SerializeField]private float _sensorUpdateTime;
        [SerializeField]private float _searchEnemyRadius;
        [SerializeField]private int _searchEnemyCount;
        [SerializeField]private float _timeNeedToAlive;

        private float _isLeft;
        private float _isRight;
        private float _isAttack;
        private float _isMove;
        private float _isUltimate;
        
        private List<Vector3> _distanceToAsteroids = new List<Vector3>();
        private Rigidbody2D _rigidbody2D;
        private Coroutine _timerCoroutine;
        
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

        public void PlayerDeath()
        {
            EndEpisode();
        }
        private void SearchEnemy()
        {
            _distanceToAsteroids.Clear();
            var listResult = new List<Collider2D>();
            var searchCountEnemy = 0;
            Physics2D.OverlapCircle(transform.position, _searchEnemyRadius, new ContactFilter2D(), listResult);
            foreach (var hitColliders2D in listResult.TakeWhile(hitColliders2D => searchCountEnemy != _searchEnemyCount && hitColliders2D.CompareTag("enemy")))
            {
                searchCountEnemy++;
                _distanceToAsteroids.Add(hitColliders2D.transform.position - transform.position);
            }

            for (var i = _distanceToAsteroids.Count; i < _searchEnemyCount; i++)
            {
                _distanceToAsteroids.Add(Vector3.zero);
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

        public override void OnActionReceived(ActionBuffers actions)
        {
            
            _isLeft = actions.ContinuousActions[0];
            _isRight = actions.ContinuousActions[1];
            _isAttack = actions.ContinuousActions[2];
            _isMove = actions.ContinuousActions[3];
            _isUltimate = actions.ContinuousActions[4];
        }



        public bool IsAttackButton()
        {
            return _isAttack >= 0.5;;
        }

        public bool IsUltimateButton()
        {
            return _isUltimate >= 0.5;;
        }

        public bool IsLeftRotationButton()
        {
            return _isLeft >= 0.5;;
        }

        public bool IsRightRotationButton()
        {
            return _isRight >= 0.5;;
        }

        public bool IsMove()
        {
            return _isMove >= 0.5;
        }

        public Vector3 Move()
        {
            return Vector3.up;
        }
    }
}