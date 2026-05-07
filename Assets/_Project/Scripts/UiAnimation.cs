using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable]
public class UiAnimation
{
    [SerializeField] private float _lerpDuration = 1f;

    [Header("Depth of Field")]
    [SerializeField] private Volume _volume;

    [SerializeField] private float _onDof = 0.5f;
    [SerializeField] private float _offDof = 3f;

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _onAlpha = 1f;
    [SerializeField] private float _offAlpha = 0f;

    private bool _isInitialized = false;
    private DepthOfField _dof;

    private void Init()
    {
        if (_isInitialized) return;
        if (!_volume.profile.TryGet(out _dof)) return;

        _isInitialized = true;
    }

    private IEnumerator InternalPlay(float alphaStart, float alphaEnd, float dofStart, float dofEnd, Action finalizeState, Action onComplete)
    {
        Init();

        float elapsed = 0f;
        _canvasGroup.alpha = alphaStart;
        _dof.focusDistance.value = dofStart;
        _dof.active = true;

        while (elapsed < _lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _lerpDuration;

            _canvasGroup.alpha = Mathf.Lerp(alphaStart, alphaEnd, t);
            _dof.focusDistance.value = Mathf.Lerp(dofStart, dofEnd, t);
            yield return null;
        }

        _canvasGroup.alpha = alphaEnd;
        _dof.focusDistance.value = dofEnd;
        finalizeState?.Invoke();
        onComplete?.Invoke();
    }

    public IEnumerator PlayOnAnimation(Action onComplete = null)
    {
        return InternalPlay(_offAlpha, _onAlpha, _offDof, _onDof, null, onComplete);
    }
    
    public IEnumerator PlayOffAnimation(Action onComplete = null)
    {
        return InternalPlay(_onAlpha, _offAlpha, _onDof, _offDof, DeactivateElements, onComplete);
    }
    
    private void DeactivateElements()
    {
        _dof.active = false;
    }
}
