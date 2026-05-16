using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DraggableSprite : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Drag Settings")] public Vector2 maxDragDistance = new Vector2(3f, 3f);

    // Add drag center offset (world units)
    public Vector2 dragCenterOffset = Vector2.zero;

    private bool _isDragging = false;
    private Vector3 _initialPosition;
    private Vector3 _dragCenter; // new: center used for clamping and gizmos
    private Vector3 _dragOffset;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _initialPosition = transform.position;
        // compute drag center from initial position plus inspector offset
        _dragCenter = _initialPosition + (Vector3)dragCenterOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPoint = GetWorldPosition(eventData);
        _dragOffset = transform.position - worldPoint;
        _isDragging = true;
        ManageCamera.OnCameraTransitionStart += OnCameraTransitionStart;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;
        Vector3 targetPosition = GetWorldPosition(eventData) + _dragOffset;
        transform.position = ClampToMaxDistance(targetPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        // unsubscribe to avoid dangling subscriptions
        ManageCamera.OnCameraTransitionStart -= OnCameraTransitionStart;
    }

    private void OnCameraTransitionStart()
    {
        _isDragging = false;
    }

    // ensure we remove subscription if object is destroyed/disabled
    private void OnDisable()
    {
        ManageCamera.OnCameraTransitionStart -= OnCameraTransitionStart;
    }

    private Vector3 GetWorldPosition(PointerEventData eventData)
    {
        Vector3 screenPos = eventData.position;
        screenPos.z = _mainCamera.WorldToScreenPoint(transform.position).z;
        return _mainCamera.ScreenToWorldPoint(screenPos);
    }

    private Vector3 ClampToMaxDistance(Vector3 targetPosition)
    {
        // compute actual allowed distance by multiplying the multiplier by the object's world scale
        Vector2 scaledMax = maxDragDistance;

        float clampedX = Mathf.Clamp(
            targetPosition.x,
            _dragCenter.x - scaledMax.x,
            _dragCenter.x + scaledMax.x
        );

        float clampedY = Mathf.Clamp(
            targetPosition.y,
            _dragCenter.y - scaledMax.y,
            _dragCenter.y + scaledMax.y
        );

        return new Vector3(clampedX, clampedY, _initialPosition.z);
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = Application.isPlaying ? _dragCenter : transform.position + (Vector3)dragCenterOffset;
#if UNITY_EDITOR
        if (Selection.gameObjects.Contains(gameObject)) Gizmos.color = new Color(0f, 1f, 1f, 0.4f);
        else Gizmos.color = new Color(0f, 1f, 1f, 0.1f);
#endif

        Vector3 scale = transform.lossyScale;
        Vector2 scaledMax = maxDragDistance + new Vector2(0.5f * scale.x, 0.2f * scale.z);
        Gizmos.DrawWireCube(origin, new Vector3(scaledMax.x * 2, scaledMax.y * 2, 0));
        Gizmos.DrawWireSphere(origin, 0.1f);
    }
}