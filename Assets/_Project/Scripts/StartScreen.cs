using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private UiAnimation _animation;
    [SerializeField] private InputActionReference _inputAction;

    private void OnEnable()
    {
        StartCoroutine(_animation.PlayOnAnimation(() => {
            _inputAction.action.Enable();
            _inputAction.action.performed += OnStartGame;
        }));
    }

    private void OnDisable()
    {
        _inputAction.action.performed -= OnStartGame;
    }

    private void OnStartGame(InputAction.CallbackContext obj)
    {
        StartCoroutine(_animation.PlayOffAnimation(() => {
            gameObject.SetActive(false);
            GameManager.StartGame();
        }));
    }
}
