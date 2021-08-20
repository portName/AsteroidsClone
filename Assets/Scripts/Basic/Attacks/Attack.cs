using System.Collections;
using Basic.Attacks.Info;
using UnityEngine;

namespace Basic.Attacks
{
    public abstract class Attack : MonoBehaviour
    {
        public abstract void Fire(AttackInfo info);
        public abstract bool IsReload();
        public abstract void Reload();
    }
}
