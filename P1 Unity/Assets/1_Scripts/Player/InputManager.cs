using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovementManager _movementManager;
    private PlayerAttackController _attackController;
    private PlayerLifeComponent _playerLife;
    private Animator _animator;

    private float _horizontal, _changeGravity, _jump;

    //invert controls properties
    private float _invertElapsedTime = 0f;
    [SerializeField]
    private float _invertDuration = 5f;

    private bool _gravButtonDown = false, _jumpButtonDown = false, _invertControl = false, _dashButtonDown = false, _shootButtonDown = false, _ampDash = false;

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

            _invertElapsedTime += Time.deltaTime;

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
        _playerLife = GetComponent<PlayerLifeComponent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        ControlManager(); // decide el control a usar

        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.Instance.PauseMenu(); // Pausa

        // Si la pausa no está activada, recoge el input
        if (Time.timeScale > 0)
        {
            _movementManager.Move(new Vector2(_horizontal, 0)); // Desplazamiento horizontal
            _animator.SetBool("run", _horizontal != 0);
            if (_changeGravity > 0 && !_gravButtonDown) // Cambio de gravedad
            {
                _movementManager.ChangeGravity();
                _gravButtonDown = true;
            }
            else if (_changeGravity == 0) _gravButtonDown = false;

            if (_jump > 0 && !_jumpButtonDown) // Salto
            {
                _movementManager.Jump();
                _jumpButtonDown = true;
            }
            else if (_jump == 0) _jumpButtonDown = false;

            if (Input.GetAxis("Shoot") > 0 && !_shootButtonDown)  // Disparo
            {
                _animator.SetBool("_shot", true);
                _attackController.Shoot(Input.GetAxis("AmpPower") > 0 && _playerLife.UseEnergy());
                _shootButtonDown = true;
            }
            else if(Input.GetAxis("Shoot") == 0)
            {
                _animator.SetBool("_shot", false);
                _shootButtonDown = false;
            }

            if(Input.GetAxis("Dash") > 0 && !_dashButtonDown) { // Deslizamiento / Dash
                _movementManager.Dash();
                if(Input.GetAxis("AmpPower") > 0 && _playerLife.UseEnergy()) _ampDash = true;
                _dashButtonDown = true;
            } else if(Input.GetAxis("Dash") == 0) _dashButtonDown = false;

            if(_ampDash) {
                if(_movementManager.Dashing) _playerLife.ActivateGrace = true;
                else _playerLife.ActivateGrace = _ampDash = false;
            }
        }             
    }
}
