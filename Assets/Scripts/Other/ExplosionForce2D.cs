using System.Collections;
using UnityEngine;

namespace Other
{
    public class ExplosionForce2D : MonoBehaviour 
    {
        private enum Mode {simple, adaptive}
        [SerializeField] private Mode mode;
        [SerializeField] private float radius;
        [SerializeField] private float power;

        public void Explosion2D(Vector3 position)
        {
            var colliders = Physics2D.OverlapCircleAll(position, radius);

            foreach(var hit in colliders)
            {
                if (hit.attachedRigidbody == null) continue;
                var direction = hit.transform.position - position;
                direction.z = 0;

                if(CanUse(position, hit.attachedRigidbody))
                {
                    hit.attachedRigidbody.AddForce(direction.normalized * power);	
                }
            }
        }

        private bool CanUse(Vector3 position, Rigidbody2D body)
        {
            if(mode == Mode.simple) return true;

            var hit = Physics2D.Linecast(position, body.position);

            return hit.rigidbody != null && hit.rigidbody == body;
        }
        
    }
}