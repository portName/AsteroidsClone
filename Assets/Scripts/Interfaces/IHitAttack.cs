using System;
using UnityEngine;

namespace Interfaces
{
    public interface IHitAttack
    {
        event Action<GameObject> HitEvent;
    }
}