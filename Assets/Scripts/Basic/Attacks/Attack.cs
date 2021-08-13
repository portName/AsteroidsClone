using System.Collections;
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
