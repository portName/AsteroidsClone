using Interfaces;
using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(IDeleteAble))]
    public class LimitDistance : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;

        private Vector3 _startPosition;
        private IDeleteAble _objectDelete;
        
        private float _distance;

        private void Start()
        {
            _startPosition = transform.position;
            _objectDelete = GetComponent<IDeleteAble>();
        }

        private void FixedUpdate()
        {
            _distance = Vector3.Distance(_startPosition, transform.position);
            if (IsOutputLimit())
            {
                OutputLimit();
            }
        }

        private bool IsOutputLimit()
        {
            return _distance >= _maxDistance;
        }

        private void OutputLimit()
        {
            _objectDelete.Delete();   
        }
    }
}