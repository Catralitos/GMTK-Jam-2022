using System;
using Audio;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool mouseControl;
        public float dashCooldown = 1f;
        public float dashSpeed = 10;
        public float dashTime = 0.5f;

        public float mouseRotationSensitivity = -1.15f;
        public float controllerPushingSensitivity = 0.75f;
        public float controllerRotationSensitivity = 1.15f;

        public float runSpeed = 20.0f;
        public int angleOffset = -90;

        private Animator _animator;
        private Camera _camera;
        private GameManager _gameManager;
        private TrailRenderer _trailRenderer;
        private PlayerControls _playerControls;
        private PlayerShooting _playerShooting;
        private Rigidbody2D _body;

        [HideInInspector] public bool canDash;
        public bool dashing;
        private bool _firing;
        private float _angle;
        private float _dashCooldownLeft;
        private float _dashLeft;
        private float _lastAngle;

        private Vector2 _dashDirection;
        private Vector2 _aim;
        private Vector2 _move;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _playerControls = new PlayerControls();
            mouseControl = _gameManager.mouseControls;
            canDash = true;
            _dashCooldownLeft = 0f;
            if (mouseControl)
            {
                _playerControls.KeyboardGameplay.Fire.performed += _ => _firing = true;
                _playerControls.KeyboardGameplay.Fire.canceled += _ => _firing = false;
                _playerControls.KeyboardGameplay.Move.performed += ctx => { _move = ctx.ReadValue<Vector2>(); };
                _playerControls.KeyboardGameplay.Move.canceled += _ => _move = Vector2.zero;
                _playerControls.KeyboardGameplay.Dash.performed += _ =>
                {
                    if (dashing || !canDash || _move.magnitude <= 0.01f) return;
                    dashing = true;
                    AudioManager.Instance.Play("Dash");
                    _animator.SetBool("Dashing", true);
                    _dashDirection = _move;
                };
            }
            else
            {
                _playerControls.ControllerGameplay.Move.performed += ctx => { _move = ctx.ReadValue<Vector2>(); };
                _playerControls.ControllerGameplay.Move.canceled += _ => { _move = Vector2.zero; };
                _playerControls.ControllerGameplay.Aim.performed += ctx =>
                {
                    var value = ctx.ReadValue<Vector2>();
                    var valueRounded = new Vector2((float) Math.Round(value.x, 2), (float) Math.Round(value.y, 2));
                    if (valueRounded.magnitude < controllerPushingSensitivity) return;
                    _aim = valueRounded;
                    _firing = true;
                };
                _playerControls.ControllerGameplay.Aim.canceled += _ => _firing = false;
                _playerControls.ControllerGameplay.Dash.started += _ =>
                {
                    if (dashing || !canDash || _move.magnitude <= 0.01f) return;
                    dashing = true;
                    _animator.SetBool("Dashing", true);
                    AudioManager.Instance.Play("Dash");
                    _dashDirection = _move;
                };
            }
        }

        private void OnEnable()
        {
            if (mouseControl) _playerControls.KeyboardGameplay.Enable();
            else _playerControls.ControllerGameplay.Enable();
        }

        private void OnDisable()
        {
            if (mouseControl) _playerControls.KeyboardGameplay.Disable();
            else _playerControls.ControllerGameplay.Disable();
        }

        private void Start()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _playerShooting = GetComponent<PlayerShooting>();
            canDash = true;
            _dashCooldownLeft = 0f;
        }

        private void Update()
        {
            _trailRenderer.emitting = dashing;
            RotateTo();
            if (_firing) _playerShooting.Shoot();
            if (dashing) _dashLeft -= Time.deltaTime;
            else _dashCooldownLeft -= Time.deltaTime;
            if (_dashCooldownLeft <= 0f) canDash = true;
            //isto e tudo so para o dash
            if (_dashLeft > 0f) return;
            _dashDirection = Vector2.zero;
            _body.velocity = Vector2.zero;
            dashing = false;
            _animator.SetBool("Dashing", false);
            _dashLeft = dashTime;
            canDash = false;
            _dashCooldownLeft = dashCooldown;
        }

        private void FixedUpdate()
        {
            _body.velocity = !dashing
                ? _move * runSpeed
                : (_dashDirection * 10).normalized * dashSpeed;
        }

        private void RotateTo()
        {
            if (mouseControl)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.localPosition);

                _aim = ((Vector2) mousePosition - (Vector2) screenPosition).normalized;
            }

            _lastAngle = _angle;
            _angle = Mathf.Atan2(_aim.y, _aim.x) * Mathf.Rad2Deg + angleOffset;
            if (!mouseControl && Mathf.Abs(_lastAngle - _angle) > controllerRotationSensitivity)
                transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            if (mouseControl && Mathf.Abs(_lastAngle - _angle) > mouseRotationSensitivity)
                transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        }
    }
}