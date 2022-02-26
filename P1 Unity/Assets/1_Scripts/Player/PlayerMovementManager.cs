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
    #endregion

    #region references
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private Trigger _trigger;
    #endregion
    public void HasAntigravity()
    {
        _hasAntigravity = true;
    }
    public void Move(Vector2 v) {
        _movement = v * _speed;
    }

    public void ChangeGravity() {
        if(_hasAntigravity && _canChangeGravity) _changingGravity = true;
    }

    public void Jump() {
        if(_jumpsLeft > 0) _jumping = true;
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

    // Start is called before the first frame update
    void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _trigger.accionEntrar = EnterGround; _trigger.accionSalir = ExitGround;
    }

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
            _canChangeGravity = _canDash = true;
            _jumpsLeft = _airJumpNumber;
        }

        // Cambio de gravedad
        if(_changingGravity) {
            _rigidbody.rotation += 180;
            _rigidbody.gravityScale = -_rigidbody.gravityScale;
            _facingRight = !_facingRight;
            _changingGravity = _canChangeGravity = false;
        }

        // Salto y doble salto
        if(_jumping) {
            if (_isGrounded) _jumpsLeft++;
            _jumpsLeft--;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, _jumpForce * _rigidbody.gravityScale / Mathf.Abs(_rigidbody.gravityScale)));
            _jumping = false;
        }

        // Deslizamiento
        if(_dashing) {
            if(_rigidbody.gravityScale != 0) _gravityScale = _rigidbody.gravityScale;
            _rigidbody.gravityScale = 0;
            if(_facingRight) _rigidbody.MovePosition(_rigidbody.position + new Vector2(_dashDistance / _dashTime * Time.fixedDeltaTime, 0));
            else _rigidbody.MovePosition(_rigidbody.position - new Vector2(_dashDistance / _dashTime * Time.fixedDeltaTime, 0));
            _dashCont += Time.fixedDeltaTime;
            if(_dashCont > _dashTime) {
                _rigidbody.gravityScale = _gravityScale;
                _dashCont = 0;
                _dashing = _canDash = false;
            }
        }
    }
}
