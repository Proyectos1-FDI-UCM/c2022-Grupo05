using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovementManager _movementManager;
    private PlayerAttackController _attackController;
    private Transform _transform;

    private float _horizontal;
    private float _changeGravity;
    private float _jump;

    //invert controls properties
    private float _invertElapsedTime = 0f;
    [SerializeField]
    private float _invertDuration;

    private bool _gravButtonDown = false, _jumpButtonDown = false, _invertControl = false;

    #region methods
    public void InvertControl()
    {
        // invierte el control cada vez que recoja una seta
        _invertControl = !_invertControl;
        _invertElapsedTime = 0f;

    }

    private void ControlManager()
    {
        if (_invertControl)
        {
            _horizontal = -Input.GetAxis("Horizontal");
            _changeGravity = Input.GetAxis("Jump");
            _jump = Input.GetAxis("ChangeGravity");

            _invertElapsedTime += 1 * Time.deltaTime;

            if (_invertElapsedTime >= _invertDuration)
            {
                _invertControl = false;
            }
        }
        else
        {
            _horizontal = Input.GetAxis("Horizontal");
            _changeGravity = Input.GetAxis("ChangeGravity");
            _jump = Input.GetAxis("Jump");
        }
    }
    #endregion 
    void Start()
    {
        _movementManager = GetComponent<PlayerMovementManager>();
        _attackController = GetComponent<PlayerAttackController>();
        _transform = transform;


    }


    void Update()
    {

        ControlManager(); // decide el control a usar

        _movementManager.Move(new Vector2(_horizontal, 0)); // Desplazamiento horizontal

        if (_changeGravity > 0 && !_gravButtonDown)
        { // Cambio de gravedad
            _movementManager.ChangeGravity();
            _gravButtonDown = true;
        }
        else if (_changeGravity == 0) _gravButtonDown = false;

        if (_jump > 0 && !_jumpButtonDown)
        { // Salto
            _movementManager.Jump();
            _jumpButtonDown = true;
        }
        else if (_jump == 0) _jumpButtonDown = false;

        if (Input.GetAxis("Shoot") > 0) _attackController.Shoot(); // Disparo



        if (Input.GetKeyDown(KeyCode.C)) _movementManager.Dash(); // Deslizamiento / Dash
    }
}
