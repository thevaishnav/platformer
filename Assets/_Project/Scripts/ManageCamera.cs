using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class ManageCamera : MonoBehaviour
{
    public static event Action OnCameraTransitionStart;
    
    [SerializeField] private float _transitionDuration = 0.25f;
    [SerializeField] private float _waitAfterTransition = 0.2f;
    [SerializeField] [Range(0f, 1f)] private float _triggerThreshold = 0.9f;

    private Transform _cameraTransform;
    private Camera _mainCamera;
    private Character _character;
    private float _currentSegmentEndX;

    private static float Aspect => Screen.width / (float)Screen.height;
    private float SegmentLength => 2f * _mainCamera.orthographicSize * Aspect;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _mainCamera = Camera.main;
        _cameraTransform = _mainCamera.transform;
        _currentSegmentEndX = _cameraTransform.position.x + (SegmentLength * _triggerThreshold) / 2f;
    }

    private void LateUpdate()
    {
        float characterX = transform.position.x;

        if (characterX > _currentSegmentEndX)
        {
            float segmentLength = SegmentLength * _triggerThreshold;
            float cameraTargetX = _cameraTransform.position.x + segmentLength;
            _currentSegmentEndX = cameraTargetX + segmentLength / 2f;
            StartCoroutine(MoveCameraToX(cameraTargetX));
        }
    }

    private IEnumerator MoveCameraToX(float endX)
    {
        OnCameraTransitionStart?.Invoke();
        _character.enabled = false;
        Vector3 current = _cameraTransform.position;
        float startX = current.x;
        float elapsedTime = 0f;

        while (elapsedTime < _transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _transitionDuration);
            current.x = Mathf.Lerp(startX, endX, t);
            _cameraTransform.position = current;
            yield return null;
        }

        current.x = endX;
        _cameraTransform.position = current;

        yield return new WaitForSeconds(_waitAfterTransition);
        _character.enabled = true;
    }
}