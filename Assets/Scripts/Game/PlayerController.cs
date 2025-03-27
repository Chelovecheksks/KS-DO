using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PhotonView))]
[RequireComponent(typeof (Collider))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float _runSpeed = 8.0f;
    [SerializeField] private float _walkSpeed = 4.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _mouseSensivity = 250.0f;
    [SerializeField] private float _maxHealth = 100.0f;

    private float _currentHealth;
    private float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; OnHealthChange?.Invoke(value); } }
    public Action<float> OnHealthChange;

    private Rigidbody _rigidbody;
    private PhotonView _playerView;
    private Collider _collider;
    private Item _item;
    private Animator _animator;

    private Camera _playerCamera;
    private Transform _cameraHolder;
    private ItemHolder _itemHolder;

    private float _horizontalInput;
    private float _verticalInput;
    private float _horizontalMouseInput;
    private float _verticalMouseInput;
    private bool _isJumpButtonDown;

    private float _currentCameraAngleX;
    private float _cameraClampAngleX = 85.0f;
    private bool _isGrounded = true;
    private bool _isJumpReady;
    private float _jumpBufferTimer;
    private float _jumpTime = 0.2f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerView = GetComponent<PhotonView>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        _playerCamera = GetComponentInChildren<Camera>();
        _cameraHolder = _playerCamera.transform.parent;
        _itemHolder = _cameraHolder.GetComponentInChildren<ItemHolder>();
        _item = _itemHolder.SelectItem(0);

        if (_playerView.IsMine == false)
        {
            Destroy(_playerCamera.gameObject);
        }

        _currentHealth = _maxHealth;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_playerView.IsMine)
        {
            PlayerInput();
            MovePlayer();
            RotatePlayer();
            UseItem();
        }
    }

    private void PlayerInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalMouseInput = Input.GetAxisRaw("Mouse X") * _mouseSensivity * Time.deltaTime;
        _verticalMouseInput = Input.GetAxisRaw("Mouse Y") * _mouseSensivity * Time.deltaTime;
        _isJumpButtonDown = Input.GetKeyDown(KeyCode.Space);

        if (_isJumpButtonDown)
        {
            _jumpBufferTimer = _jumpTime;
            _isJumpReady = true;
        }

        if (_jumpBufferTimer <= 0.0f)
        {
            _isJumpReady = false;
        }
        else
        {
            _jumpBufferTimer -= Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        Vector3 direction = new Vector3(_horizontalInput * _runSpeed, 
                                        _rigidbody.velocity.y, 
                                        _verticalInput * _runSpeed);
        direction = transform.TransformDirection(direction);
        _rigidbody.velocity = direction;

        if (_isJumpReady && _isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 
                                              _jumpForce, 
                                              _rigidbody.velocity.z);
        }

        Ray ray = new Ray(_collider.bounds.min, Vector3.down);
        _isGrounded = Physics.Raycast(ray, out RaycastHit hit, 0.3f);

    }

    private void RotatePlayer()
    {
        transform.Rotate(0, _horizontalMouseInput, 0);

        _currentCameraAngleX -= _verticalMouseInput;
        _currentCameraAngleX = Mathf.Clamp(_currentCameraAngleX, -_cameraClampAngleX, _cameraClampAngleX);
        _cameraHolder.transform.localEulerAngles = new Vector3(_currentCameraAngleX, 0, 0);
    }

    public void TakeDamage(float damage)
    {
        _playerView.RPC("RemoteTakeDamage", RpcTarget.All, damage);

    }

    [PunRPC]
    private void RemoteTakeDamage(float damage)
    {
        CurrentHealth -= damage;
        _animator.Play("Hurt");
    }

    private void UseItem()
    {
        if (_item.GetType() == typeof(Weapon))
        {
            if(Input.GetMouseButtonDown(0))
            {
                ((Weapon)_item).Use();
            }
        }
    }
}
