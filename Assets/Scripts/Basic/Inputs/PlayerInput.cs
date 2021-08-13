using System;
using UI.Buttons;
using UnityEngine;

namespace Basic.Inputs
{
    internal class PlayerInput : CustomInput
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _ultimateButton;
        [SerializeField] private Button _leftRotationButton;
        [SerializeField] private Button _rightRotationButton;
        [SerializeField] private Button _moveButton;
        
        public override bool IsAttackButton()
        {
            return _attackButton.IsDown();
        }

        public override bool IsUltimateButton()
        {
            return _ultimateButton.IsDown();
        }

        public override bool IsLeftRotationButton()
        {
            return _leftRotationButton.IsDown();
        }

        public override bool IsRightRotationButton()
        {
            return _rightRotationButton.IsDown();
        }

        public override bool IsMove()
        {
            return _moveButton.IsDown();
        }

        public override Vector3 Move()
        {
            return transform.up;
        }
    }
}