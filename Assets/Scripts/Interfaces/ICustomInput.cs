using UnityEngine;

namespace Interfaces
{
    public interface ICustomInput
    {
          bool IsAttackButton();
          bool IsUltimateButton();
          bool IsLeftRotationButton();
          bool IsRightRotationButton();
          bool IsMove();
          Vector3 Move();
    }
}