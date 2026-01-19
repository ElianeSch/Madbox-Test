using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Joystick : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickBG;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private float maxRadius = 80f;
    [SerializeField] private float deadzone = 0.05f;
    public event Action<Vector2> OnDirectionChanged;
    private Vector2 handleStartPos;
    private Vector2 pointerStartLocalPos;

    void Awake()
    {
        handleStartPos = joystickHandle.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG,
            eventData.position,
            eventData.pressEventCamera,
            out pointerStartLocalPos
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 currentLocalPos
        );

        Vector2 delta = currentLocalPos - pointerStartLocalPos;
        Vector2 clamped = Vector2.ClampMagnitude(delta, maxRadius);

        joystickHandle.anchoredPosition = handleStartPos + clamped;

        Vector2 dir = -clamped / maxRadius;
        if (dir.sqrMagnitude < deadzone * deadzone)
            dir = Vector2.zero;
        OnDirectionChanged?.Invoke(dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        joystickOutline.localRotation =
            Quaternion.Euler(0f, 0f, angle - 45f + 180f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = handleStartPos;
        OnDirectionChanged?.Invoke(Vector2.zero);
    }
}
