using Basic;
using Basic.Movements;
using UnityEngine;

namespace ArtificialIntelligences
{
    public class FlyingSaucerAI : AI
    {
        private Player _player;
        
        private void Start()
        {
            _player = Player.Instance;
        }

        private void FixedUpdate()
        {
            
        }
        
        private Vector3 DirectionToPlayer()
        {
            var player = Player.Instance;
            var distance = player.transform.position - transform.position;
            return distance.normalized;
        }
    }
}