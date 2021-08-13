using System;
using Basic.Inputs;
using UnityEngine;

namespace Basic.Movements
{
    public abstract class Movement : MonoBehaviour
    {

        public abstract void Move(Vector3 move);
        public abstract void Move(Vector3 move, ForceMode2D forceMode2D);
        public abstract void Rotate(Vector3 rotate);
        public abstract void Limit(float limitVelocityMagnitude);
        
    }
}
