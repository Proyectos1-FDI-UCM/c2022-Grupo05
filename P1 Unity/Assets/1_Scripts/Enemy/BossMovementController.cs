using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashSpeed = 12f;
    [SerializeField] private float _actionTime = 3f;
    [SerializeField] private float _dashTime = 1f;
    #endregion

    #region properties
    private float _timer;
    private float _dashTimer;
    private bool _gravityHasChanged;
    private bool _changingGravity;
    private int _direction;
    #endregion

    #region references
    [SerializeField] private Transform _playerPos;
    [SerializeField] private PlayerMovementManager _gravityCheck;
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    #endregion

    #region methods
    private void Action() 
    {
        if (_gravityHasChanged != _gravityCheck.IsGravityChanged)
        {
            _changingGravity = true;
        }
        else _dashTimer = _dashTime;
        _timer = 0;
    }

    private void SetDirection() 
    {
        int dir = _playerPos.position.x > _myTransform.position.x ? 1 : -1;
        if (dir != _direction) 
        {
            _myTransform.Rotate(0, 180, 0);
            _direction = dir;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        _timer = 0;
        _direction = _playerPos.position.x > _myTransform.position.x ? 1 : -1;
    }

    private void FixedUpdate()
    {
        _myRB.velocity = new Vector2(_direction * (_dashTimer > 0 ? _dashSpeed : _speed), _myRB.velocity.y);

        if (_changingGravity)
        {
            _myTransform.localScale = new Vector3(_myTransform.localScale.x, -_myTransform.localScale.y, _myTransform.localScale.z);
            _myRB.gravityScale = -_myRB.gravityScale;
            _changingGravity =  false;
            _gravityHasChanged = !_gravityHasChanged;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < _actionTime) _timer += Time.deltaTime;
        else Action();

        if (_dashTimer > 0) _dashTimer -= Time.deltaTime;


        SetDirection();
    }
}
