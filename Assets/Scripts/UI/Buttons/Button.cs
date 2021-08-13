using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Buttons
{
    public class Button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private bool _buttonDown;
        public virtual void Do(){}

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonDown = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonDown = true;
        }

        public virtual bool IsDown()
        {
            return _buttonDown;
        }
    }
}