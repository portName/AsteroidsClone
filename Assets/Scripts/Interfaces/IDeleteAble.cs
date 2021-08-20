using System;
using UnityEngine;

namespace Interfaces
{
    public interface IDeleteAble
    {
         void Delete();
         event Action<GameObject> DeleteGameObjectEvent;
    }
}