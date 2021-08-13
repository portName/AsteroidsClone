using Interfaces;
using UnityEngine;

namespace Basic.Inputs
{
    public abstract class CustomInput : MonoBehaviour, ICustomInput
    {
        public abstract bool IsAttackButton();
        public abstract bool IsUltimateButton();
        public abstract bool IsLeftRotationButton();
        public abstract bool IsRightRotationButton();
        public abstract bool IsMove();
        public abstract Vector3 Move();
    }
}