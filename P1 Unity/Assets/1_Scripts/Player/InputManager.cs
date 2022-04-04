using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovementManager _movementManager;
    private PlayerAttackController _attackController;
    private PlayerLifeComponent _playerLife;
    private Animator _animator;
    private SpriteRenderer _rend ;

    private float _horizontal, _changeGravity, _jump;
    

    //invert controls properties
    private float _invertElapsedTime = 0f;
    [SerializeField]
    private float _invertDuration = 5f;

    private bool _gravButtonDown = false, _jumpButtonDown = false, _invertControl = false, _dashButtonDown = false;
    private bool _shotEnabled;

    #region methods
    public void HasShot(bool enable)
    {
        _shotEnabled=enable;
    }
    public void InvertControl()
    {
        // invierte el control cada vez que recoja una seta
        _invertControl = !_invertControl;
        _invertElapsedTime = 0f;
        if (_invertControl)
            _rend.color = new Color(0.8627451f, 1, 0.2705882f, 1);
        else _rend.color = new Color(1, 1, 1, 1);


    }

    public void ChangeColor()
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
               _rend.color = new Color(1, 1, 1, 1);
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
        _rend = GetComponent<SpriteRenderer>();
        _shotEnabled =true;
  
    }

    void Update()
    {
        ControlManager(); // decide el control a usar

        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.Instance.PauseMenu(false); // Pausa
        else if (Input.GetKeyDown(KeyCode.JoystickButton9)) GameManager.Instance.PauseMenu(true); // Pausa


        // Si la pausa no está activada, recoge el input
        if (Time.timeScale > 0)
        {
            _movementManager.Move(new Vector2(_horizontal, 0)); // Desplazamiento horizontal
            
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

            if (Input.GetAxis("Shoot") > 0 && _shotEnabled && !_movementManager.Dashing)  // Disparo
            {
                _animator.SetBool("_shot", true);
                _attackController.Shoot(Input.GetAxis("AmpPower") > 0 && _attackController.ShootReady && _playerLife.UseEnergy());
            }
            else if(Input.GetAxis("Shoot") == 0)
            {
                _animator.SetBool("_shot", false);
            }

            if (Input.GetAxis("Dash") > 0 && !_dashButtonDown) { // Deslizamiento / Dash
                _movementManager.Dash();
                if(Input.GetAxis("AmpPower") > 0 && _playerLife.UseEnergy()) _playerLife.AmpDash = true;
                _dashButtonDown = true;
            } else if(Input.GetAxis("Dash") == 0) _dashButtonDown = false;
        }             
    }
}
