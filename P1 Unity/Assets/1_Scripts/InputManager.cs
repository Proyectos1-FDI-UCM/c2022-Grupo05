using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovementManager _movementManager;
    private Transform _transform;

    private bool _gravButtonDown = false, _jumpButtonDown = false;

    void Start() {
        _movementManager = GetComponent<PlayerMovementManager>();
        _transform = transform;
    }

    void Update() {
        _movementManager.Move(new Vector2(Input.GetAxis("Horizontal"), 0)); // Desplazamiento horizontal

        if(Input.GetAxis("ChangeGravity") > 0 && !_gravButtonDown) { // Cambio de gravedad
            _movementManager.ChangeGravity();
            _gravButtonDown = true;
        } else if(Input.GetAxis("ChangeGravity") == 0) _gravButtonDown = false;

        if(Input.GetAxis("Jump") > 0 && !_jumpButtonDown) { // Salto
            _movementManager.Jump();
            _jumpButtonDown = true;
        } else if(Input.GetAxis("Jump") == 0) _jumpButtonDown = false;

        if(Input.GetKeyDown(KeyCode.C)) _movementManager.Dash(); // Deslizamiento / Dash
    }
}
