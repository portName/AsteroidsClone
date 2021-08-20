using UnityEngine;

namespace Basic.Attacks.Info
{
    public struct HitInfo
    {
        public float damage;
        public float remainingHealth;
        public float distance;
        public bool isDeath;

        public int IsDeath()
        {
            return isDeath ? 1 : 0;
        }
    }
}