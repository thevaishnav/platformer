using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableSprite : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Drag Settings")] 
    public Vector2 maxDragDistance = new Vector2(3f, 3f);
    
    private bool _isDragging = false;
    private Vector3 _initialPosition;
    private Vector3 _dragOffset;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _initialPosition = transform.position;
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
    }

    private void OnCameraTransitionStart()
    {
        _isDragging = false;
    }
    
    private Vector3 GetWorldPosition(PointerEventData eventData)
    {
        Vector3 screenPos = eventData.position;
        screenPos.z = _mainCamera.WorldToScreenPoint(transform.position).z;
        return _mainCamera.ScreenToWorldPoint(screenPos);
    }

    private Vector3 ClampToMaxDistance(Vector3 targetPosition)
    {
        float clampedX = Mathf.Clamp(
            targetPosition.x,
            _initialPosition.x - maxDragDistance.x,
            _initialPosition.x + maxDragDistance.x
        );

        float clampedY = Mathf.Clamp(
            targetPosition.y,
            _initialPosition.y - maxDragDistance.y,
            _initialPosition.y + maxDragDistance.y
        );

        return new Vector3(clampedX, clampedY, _initialPosition.z);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = Application.isPlaying ? _initialPosition : transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(origin, new Vector3(maxDragDistance.x * 2, maxDragDistance.y * 2, 0));
    }
}