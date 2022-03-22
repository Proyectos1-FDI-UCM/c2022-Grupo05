using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirstPhaseController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _dashSpeed = 10f;
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
    [SerializeField] GameObject _shotPrefab;
    private Transform _playerPos;
    private PlayerMovementManager _gravityCheck;
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    #endregion

    #region methods
    private void Action() 
    {
        if (Random.Range(0,2) == 0) 
        {
            Shoot();
            _timer = 0;
        }
        else
        { 
            if (_gravityHasChanged != _gravityCheck.IsGravityChanged)
            {
                _changingGravity = true;
                _timer = _actionTime/2;

            }
            else 
            {
                _dashTimer = _dashTime;
                _timer = 0;
            }
        }
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

    private void Shoot()
    {
        {
            GameObject shotA = Instantiate(_shotPrefab, _myTransform.position, _myTransform.rotation);
            GameObject shotB = Instantiate(_shotPrefab, _myTransform.position, _myTransform.rotation);
            GameObject shotC = Instantiate(_shotPrefab, _myTransform.position, _myTransform.rotation);
            Vector3 dir = Vector3.Normalize(_playerPos.position - transform.position);
            shotA.GetComponent<ShotMovementController>().SetDirection(dir);
            shotB.GetComponent<ShotMovementController>().SetDirection(Quaternion.AngleAxis(20,Vector3.forward) * dir);
            shotC.GetComponent<ShotMovementController>().SetDirection(Quaternion.AngleAxis(-20, Vector3.forward) * dir);
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        _timer = 0;
        _playerPos = PlayerAccess.Instance.Transform;
        _direction = _playerPos.position.x > _myTransform.position.x ? 1 : -1;
        _gravityCheck = PlayerAccess.Instance.Movement;
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
        else 
        {
            SetDirection();
            Action();
        }
        if (_dashTimer > 0) _dashTimer -= Time.deltaTime;


        SetDirection();
    }
}