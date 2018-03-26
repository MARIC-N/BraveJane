using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MovementSpeed = 3.0f;
    public float JumpForce = 500.0f;
    public Transform GroundCheckObject;
    public LayerMask GroundLayerMask;
    public GameObject LeftBullet, RightBullet;
    public string MovingPlatforTagName = "MovingPlatform";


    private Transform _transform;
    private Rigidbody2D _rigidybody2D;
    private float _movementHorizontal;
    private float _movementVertical;
    private Animator _animator;

    private bool _isWalking = false;
    private bool _isFacingRight = true;
    private bool _isGrounded = true;

    private int _playerLayer;
    private int _movingPlatformLayer;

    private Transform _shootPosition;

    private void Awake()
    {
        _transform = transform;
        _rigidybody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _playerLayer = LayerMask.NameToLayer("Player");
        _movingPlatformLayer = LayerMask.NameToLayer("MovingPlatform");

        _shootPosition = transform.Find("ShootPosition");
    }

    private void Update()
    {
        if (Input.GetKeyDown("right shift"))
        {
            Shoot();
        }
        if (Input.GetKeyUp("right shift"))
        {
            _animator.SetBool("IsShooting", false);
        }
        _movementHorizontal = Input.GetAxisRaw("Horizontal");

        if (_movementHorizontal != 0)
            _isWalking = true;
        else
            _isWalking = false;

        _animator.SetBool("IsWalking", _isWalking);

        _movementVertical = _rigidybody2D.velocity.y;

        _isGrounded = Physics2D.Linecast(_transform.position, GroundCheckObject.position, GroundLayerMask);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidybody2D.AddForce(Vector2.up * JumpForce);
        }

        _animator.SetBool("IsGrounded", _isGrounded);

        if (Input.GetButtonUp("Jump") && (_movementVertical > 0.0f))
            _movementVertical = 0.0f;

        Vector2 movement = new Vector2(_movementHorizontal * MovementSpeed, _movementVertical);

        _rigidybody2D.velocity = movement;
        Physics2D.IgnoreLayerCollision(_playerLayer, _movingPlatformLayer, _movementVertical > 0.0f);

    }

    private void Shoot()
    {
        if (_isFacingRight)
            Instantiate(RightBullet, _shootPosition.position, Quaternion.identity);
        else
            Instantiate(LeftBullet, _shootPosition.position, Quaternion.identity);

        _animator.SetBool("IsShooting", true);
    }

    private void LateUpdate()
    {
        Vector3 localScale = _transform.localScale;

        if (_movementHorizontal == -1.0f)
            _isFacingRight = false;
        else if (_movementHorizontal == 1.0f)
            _isFacingRight = true;

        if (((localScale.x < 0) && _isFacingRight) || ((localScale.x > 0) && !_isFacingRight))
            localScale.x *= -1.0f;

        _transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GroundDeath"))
        {
            _animator.SetBool("IsDead", true);
            return;
        }
        if (other.gameObject.CompareTag(MovingPlatforTagName))
            transform.parent = other.transform;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(MovingPlatforTagName))
            transform.parent = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, GroundCheckObject.position);
    }
}
