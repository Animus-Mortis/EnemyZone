using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Controller
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform joystickBody;
        [SerializeField] private RectTransform joystickHandle;

        private Vector2 inputVector = Vector2.zero;

        public float HorizontalAxis { get { return inputVector.x; } }
        public float VerticalAxis { get { return inputVector.y; } }

        private void Start()
        {
            joystickBody.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBody, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x = (pos.x / joystickBody.sizeDelta.x);
                pos.y = (pos.y / joystickBody.sizeDelta.y);

                inputVector = new Vector2(pos.x * 2, pos.y * 2);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                joystickHandle.anchoredPosition = new Vector2(inputVector.x * (joystickBody.sizeDelta.x / 3), inputVector.y * (joystickBody.sizeDelta.y / 3));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            joystickBody.position = eventData.position;
            joystickBody.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData = null)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
            joystickBody.gameObject.SetActive(false);
        }
    }
}