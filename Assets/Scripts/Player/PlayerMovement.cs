using System;
using Audio;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool mouseControl;

        public float mouseRotationSensitivity = -1.15f;
        public float controllerPushingSensitivity = 0.75f;
        public float controllerRotationSensitivity = 1.15f;

        public float runSpeed = 20.0f;
        public float speedBuffMultiplier = 2.0f;
        public int angleOffset = -90;

        public GameObject firePoint;
        public GameObject lightPoint;

        private Animator _animator;

        private Camera _camera;

        //private GameManager _gameManager;
        private PlayerControls _playerControls;
        private PlayerShooting _playerShooting;
        private Rigidbody2D _body;

        private bool _firing;
        private float _angle;
        private float _lastAngle;

        private Vector2 _aim;
        private Vector2 _move;
        private Vector2 _moveV;
        private Vector2 _moveH;

        private float adjustedAngle;
        private void Awake()
        {
            //_gameManager = GameManager.Instance;
            _playerControls = new PlayerControls();
            //mouseControl = _gameManager.mouseControls;
            if (mouseControl)
            {
                _playerControls.KeyboardGameplay.Fire.performed += _ => _firing = true;
                _playerControls.KeyboardGameplay.Fire.canceled += _ => _firing = false;
                _playerControls.KeyboardGameplay.MoveVertical.performed +=
                    ctx => { _moveV = ctx.ReadValue<Vector2>(); };
                _playerControls.KeyboardGameplay.MoveVertical.canceled += _ => _moveV = Vector2.zero;
                _playerControls.KeyboardGameplay.MoveHorizontal.performed +=
                    ctx => { _moveH = ctx.ReadValue<Vector2>(); };
                _playerControls.KeyboardGameplay.MoveHorizontal.canceled += _ => _moveH = Vector2.zero;
            }
            else
            {
                _playerControls.ControllerGameplay.Move.performed += ctx => { _move = ctx.ReadValue<Vector2>(); };
                _playerControls.ControllerGameplay.Move.canceled += _ => { _move = Vector2.zero; };
                _playerControls.ControllerGameplay.Aim.performed += ctx =>
                {
                    var value = ctx.ReadValue<Vector2>();
                    var valueRounded = new Vector2((float)Math.Round(value.x, 2), (float)Math.Round(value.y, 2));
                    if (valueRounded.magnitude < controllerPushingSensitivity) return;
                    _aim = valueRounded;
                    _firing = true;
                };
                _playerControls.ControllerGameplay.Aim.canceled += _ => _firing = false;
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
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _playerShooting = GetComponent<PlayerShooting>();
        }

        private void Update()
        {
            RotateTo();
            if (_firing) _playerShooting.Shoot();
        }

        private void FixedUpdate()
        {
            float actualMoveSpeed = PlayerEntity.Instance.buffs.speedBuffTimeLeft > 0
                ? runSpeed * speedBuffMultiplier
                : runSpeed;
            if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.MovementSpeed)) {
                actualMoveSpeed *= PlayerSkills.instance.movementSpeedFactorOnUpgarde;
            }
            if (mouseControl)
            {
                _body.velocity = (_moveV + _moveH) * actualMoveSpeed;
            }
            else
            {
                _body.velocity = _move * actualMoveSpeed;
            }

            _animator.SetFloat("Speed", _body.velocity.magnitude);
        }

        private void RotateTo()
        {
            if (mouseControl)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.localPosition);

                _aim = ((Vector2)mousePosition - (Vector2)screenPosition).normalized;
            }

            _lastAngle = _angle;
            _angle = Mathf.Atan2(_aim.y, _aim.x) * Mathf.Rad2Deg + angleOffset;
            adjustedAngle = Mathf.Atan2(-_aim.y, -_aim.x) * Mathf.Rad2Deg + angleOffset;

            _animator.SetFloat("AimY", _aim.y);
            _animator.SetFloat("AimX", _aim.x);
            _animator.SetFloat("Angle", _angle);

            if ((!mouseControl && Mathf.Abs(_lastAngle - _angle) > controllerRotationSensitivity)
                || (mouseControl && Mathf.Abs(_lastAngle - _angle) > mouseRotationSensitivity))
            {
                firePoint.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
                lightPoint.transform.rotation = Quaternion.AngleAxis(adjustedAngle, Vector3.forward);
            }
        }
    }
}