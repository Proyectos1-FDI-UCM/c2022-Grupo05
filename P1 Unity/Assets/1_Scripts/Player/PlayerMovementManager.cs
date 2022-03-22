using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _jumpForce = 400f;
    [SerializeField]
    private float _movementSmoothing = 0.05f;
    [SerializeField]
    private bool _facingRight = true;
    [SerializeField]
    private float _dashDistance = 3f;
    [SerializeField]
    private float _dashTime = 0.125f;
    [SerializeField]
    private int _airJumpNumber = 1;
    [SerializeField]
    private bool _hasAntigravity = false;
    #endregion

    #region properties
    private Vector2 _movement;
    private Vector3 _velocity;
    private int _jumpsLeft;
    private bool _jumping;
    private bool _canChangeGravity;
    private bool _changingGravity;
    private bool _canDash;
    private bool _dashing;
    private float _dashCont;
    private float _gravityScale;
    private bool _isGrounded;
    private bool _isGravityChanged = false;
    public bool IsGravityChanged {
        get => _isGravityChanged;
    }
    public bool Dashing {
        get => _dashing;
    }
    #endregion

    #region references
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private FloorTrigger _trigger;
    private PlayerAnimation _animation;

    [SerializeField] private AudioClip _dashClip;
    #endregion

    #region methods
    public void HasAntigravity(bool enable)
    {
        _hasAntigravity = enable;
    }
    
    public void Move(Vector2 v) {
        _movement = v * _speed;
        _animation.Run(v.x != 0);
    }

    public void ChangeGravity() {
        if(_hasAntigravity && _canChangeGravity && !_dashing) _changingGravity = true;
    }

    public void ForceChangeGravity() {
        _changingGravity = true;
    }

    public void Jump() 
    {
        if (_jumpsLeft > 0 && !_dashing)
        {
            _jumping = true;
        }
    }

    public void Dash() {
        if(_canDash) _dashing = true;
    }

    public void EnterGround() {
        _isGrounded = true;
    }

    public void ExitGround() {
        _isGrounded = false;
    }

    public void StopDashing() {
        _dashing = false;
        _dashCont = 0;
    }

    void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _trigger.accionEntrar = EnterGround; _trigger.accionSalir = ExitGround;
        _animation = GetComponent<PlayerAnimation>();
    }
    #endregion

    private void FixedUpdate() {
        //Desplazamiento horizontal
        Vector3 targetVelocity = new Vector2(_movement.x * 10f, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        // Girar sprite si cambia de sentido
        if (!_dashing && ((_movement.x < 0 && _facingRight) || (_movement.x > 0 && !_facingRight))) {
            _facingRight = !_facingRight;
            _transform.Rotate(0, 180, 0);
            //_transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        }

        if (_isGrounded) { // Si toca el suelo, se recargan sus saltos, el cambio de gravedad y el deslizamiento
            _animation.Jump(0);
            _canChangeGravity = _canDash = true;
            _jumpsLeft = _airJumpNumber;
        }

        // Cambio de gravedad
        if(_changingGravity) {
            _transform.localScale = new Vector3(_transform.localScale.x, -_transform.localScale.y, _transform.localScale.z);
            _rigidbody.gravityScale = -_rigidbody.gravityScale;
            _changingGravity = _canChangeGravity = false;
            _isGravityChanged = !_isGravityChanged;
            HUDController.Instance.ChangePosition(_isGravityChanged);
        }

        // Salto y doble salto
        if(_jumping) 
        {
            if (!_isGrounded)
            {
                _jumpsLeft--;
            }
            _animation.Jump(_jumpsLeft == 0 ? 2 : 1);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            if(_isGravityChanged) _rigidbody.AddForce(new Vector2(0, -_jumpForce));
            else _rigidbody.AddForce(new Vector2(0, _jumpForce));
            _jumping = false;
        }

        // Deslizamiento
        if(_dashing) {
            if(_rigidbody.gravityScale != 0) { // Inicio
                SoundManager.Instance.PlaySound(_dashClip);

                _gravityScale = _rigidbody.gravityScale;
                _rigidbody.gravityScale = 0;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            }

            if(_dashCont < _dashTime) { // Durante
                if(_facingRight) _rigidbody.MovePosition(_rigidbody.position + new Vector2(_dashDistance / _dashTime * Time.fixedDeltaTime, 0));
                else _rigidbody.MovePosition(_rigidbody.position - new Vector2(_dashDistance / _dashTime * Time.fixedDeltaTime, 0));
                _dashCont += Time.fixedDeltaTime;
            } else { // Final
                _rigidbody.gravityScale = _gravityScale;
                _dashCont = 0;
                _dashing = _canDash = false;
            }
        }
    }
}
