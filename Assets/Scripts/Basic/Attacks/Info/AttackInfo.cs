using System.Collections.Generic;
using UnityEngine;

namespace Basic.Attacks.Info
{
    public class AttackInfo
    {
        public AttackInfo(GameObject owner)
        {
            _hitsInfo = new List<HitInfo>();
            Owner = owner;
        }
        
        public float Damage { get; set; }

        public GameObject Owner { get; }
        
        private readonly List<HitInfo> _hitsInfo;
        
        private HitInfo _lastHitInfo;

        public bool IsOwner(GameObject ownerGameObject)
        {
           return Owner == ownerGameObject;
        }
        
        public void AddHit(HitInfo hitInfo)
        {
            _lastHitInfo = hitInfo;
            _hitsInfo.Add(_lastHitInfo);
        }

        public List<HitInfo> AllHits()
        {
            return _hitsInfo;
        }
        public HitInfo LastHit()
        {
            return _lastHitInfo;
        }
        public float DistanceFromHitObject(Transform hitObject)
        {
            return Vector3.Distance(Owner.transform.position, hitObject.position);
        }
    }
}