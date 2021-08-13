using System;
using Interfaces;
using UnityEngine;

namespace Other
{
    public class Deleted : MonoBehaviour, IDeleteAble
    {
        public event Action<GameObject> DeleteGameObjectEvent;
        public void Delete()
        {
            Destroy(gameObject);
            DeleteGameObjectEvent?.Invoke(gameObject);
        }
    }
}