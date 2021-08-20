using Basic.Attacks.Info;
using UnityEngine;

namespace Interfaces
{
    public interface IHitCallback
    {
        void HitCallback(HitInfo hitInfo);
    }
}