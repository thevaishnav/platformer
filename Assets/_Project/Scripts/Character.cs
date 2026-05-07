using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static readonly int DIE = Animator.StringToHash("die");
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _initialWait = 1.2f;
    [SerializeField, Min(0f)] private float _moveAllowedLandingVelocity = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private Animator _animation;

    public bool canMove = false;
    private Rigidbody2D _rb2D;
    private bool _isGrounded = false;
    private Transform _currentPlatform;
    private Vector3 _currentPlatformLastPosition;
    private readonly int _isGroundedHash = Animator.StringToHash("grounded");

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        canMove = false;
        yield return new WaitForSeconds(_initialWait);
        canMove = true;
    }

    private void Update()
    {
        UpdateGroundedState();

        if (canMove && _isGrounded)
        {
            Vector3 translation = Vector3.right * (_moveSpeed * Time.deltaTime);
            var currentPlatformDelta = _currentPlatform.position - _currentPlatformLastPosition;
            translation.y += currentPlatformDelta.y;
            _currentPlatformLastPosition = _currentPlatform.position;
            transform.Translate(translation);
        }
    }

    private void UpdateGroundedState()
    {
        var hitCollider = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        _isGrounded = hitCollider != null;
        _animation.SetBool(_isGroundedHash, _isGrounded);

        if (_isGrounded)
        {
            Transform newPlatform = hitCollider.transform;
            if (_currentPlatform == newPlatform) return;
            
            if (_currentPlatform == null) OnLand(newPlatform);
            _currentPlatform = newPlatform;
            _currentPlatformLastPosition = _currentPlatform.position;
        }
        else
        {
            _currentPlatform = null;
            _currentPlatformLastPosition = Vector3.zero;
        }
    }

    private void OnLand(Transform platform)
    {
        var vel = _rb2D.linearVelocity;
        if (Mathf.Abs(vel.y) > _moveAllowedLandingVelocity)
        {
            Die();
        }
    }

    private void Die()
    {
        canMove = false;
        _animation.SetTrigger(DIE);
        GameManager.GameOver((int)(transform.position.x * 10f));
    }
}