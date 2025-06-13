using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private float handleRange = 1f;

    private Vector2 inputVector = Vector2.zero;
    private CanvasGroup canvasGroup;

    public Vector2 Direction => inputVector;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HideJoystick();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x /= joystickBackground.sizeDelta.x;
            position.y /= joystickBackground.sizeDelta.y;
            
            inputVector = new Vector2(position.x * 2, position.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            
            joystickHandle.anchoredPosition = new Vector2(
                inputVector.x * (joystickBackground.sizeDelta.x * handleRange / 2),
                inputVector.y * (joystickBackground.sizeDelta.y * handleRange / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickBackground.anchoredPosition = eventData.position;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        HideJoystick();
    }

    private void HideJoystick()
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }
}