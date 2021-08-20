using System;
using UnityEngine;

namespace Basic
{
    public class Following : MonoBehaviour
    {
        [SerializeField] private Transform _targer;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _targer.position, Time.deltaTime);
        }
    }
}