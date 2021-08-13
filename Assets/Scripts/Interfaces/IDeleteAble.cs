using System;
using UnityEngine;

namespace Interfaces
{
    public interface IDeleteAble
    {
        public void Delete();
        public event Action<GameObject> DeleteGameObjectEvent;
    }
}