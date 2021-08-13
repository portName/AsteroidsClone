using System;
using Interfaces;
using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(Collider2D))]
    public class DeleteZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var deleteAble = other.gameObject.GetComponent<IDeleteAble>();
            deleteAble?.Delete();
        }
    }
}