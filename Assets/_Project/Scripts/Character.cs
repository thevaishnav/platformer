using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static readonly int DIE = Animator.StringToHash("die");
    private static readonly int HARD_LAND = Animator.StringToHash("hardLand");
    private static readonly int VICTORY = Animator.StringToHash("victory");

// @formatter:off
    [Header("Movement Settings")]
    [SerializeField] private float _initialWait = 1.2f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _hardLandWait = 1f;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private Animator _animation;

    [Header("Height Settings (Platform Height is 1)")]
    [SerializeField, Min(0f)] private float _deathFallHeight = 8f;
    [SerializeField, Min(0f)] private float _hardLandHeight = 1f;
    [SerializeField, Min(0f)] private float _deathLandHeight = 2f;
// @formatter:on

    [NonSerialized] public bool canMove = false;
    
    private bool _isDead = false;
    private Rigidbody2D _rb2D;
    private bool _isGrounded = false;
    private Transform _currentPlatform;
    private Vector3 _currentPlatformLastPosition;
    private readonly int _isGroundedHash = Animator.StringToHash("grounded");

    private float CurrentFallHeight
    {
        get
        {
            var vel = _rb2D.linearVelocity.y;
            if (vel >= 0f) return 0f;
            var height = -(vel * vel) / (2f * Physics2D.gravity.y);
            Debug.Log("Current Fall Height: " + height);
            return height;
        }
    }

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
        if (_isGrounded)
        {
            if (!canMove) return;

            Vector3 translation = Vector3.right * (_moveSpeed * Time.deltaTime);
            var currentPlatformDelta = _currentPlatform.position - _currentPlatformLastPosition;
            translation.y += currentPlatformDelta.y;
            _currentPlatformLastPosition = _currentPlatform.position;
            transform.Translate(translation);
        }
        else
        {
            if (CurrentFallHeight > _deathFallHeight)
                Die();
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
        var fallHeight = CurrentFallHeight;
        if (fallHeight > _deathLandHeight) Die();
        else if (fallHeight > _hardLandHeight) HardLand();
    }

    private void HardLand()
    {
        canMove = false;
        _animation.SetTrigger(HARD_LAND);
        
        CancelInvoke(nameof(ResumeMovement));
        Invoke(nameof(ResumeMovement), _hardLandWait);
    }

    private void ResumeMovement()
    {
        canMove = true;
    }

    private void Die()
    {
        if (_isDead) return;
        canMove = false;
        _animation.SetTrigger(DIE);
        GameManager.Instance.GameOver(GetScoreFromPosition(transform.position.x));
        enabled = false;
        _isDead = true;
    }
    
    public static int GetScoreFromPosition(float xPosition)
    {
        return (int)(xPosition * 10f);
    }
}