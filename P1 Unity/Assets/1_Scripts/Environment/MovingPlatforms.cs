using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour {
    #region parameters
    [SerializeField]
    private float _speed = 2;          // Velocidad de movimiento 
    [SerializeField]
    private float _distance = 8;       // Distancia de movimiento
    #endregion

    #region properties
    private Vector2 _placeOrigin;      // Posici�n origen
    private Vector2 _placeObject;      // Posici�n final
    private Vector2 _dir;              // Vector de direcci�n
    [SerializeField]
    private bool _playerOn;            // Si el jugador est� encima de la plataforma
    #endregion

    #region references
    private Transform _platform;
    private Rigidbody2D _player;
    #endregion

    private void Start() {
        _platform = transform;
        _player = PlayerAccess.Instance.Rigidbody;
        _placeOrigin = _platform.position;
        _placeObject = (Vector2)_platform.position + new Vector2(_distance, 0);
        _dir = _placeObject - _placeOrigin;
    }

    // Si el jugador toca la plataforma, su transform pasa a ser hijo del transform de la plataforma
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponentInParent<PlayerMovementManager>() != null) {
            _player.transform.SetParent(gameObject.transform, true);

        }

    }

    // Si el jugador deja de tocar la plataforma, su transform deja de ser hijo del transform de la plataforma
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponentInParent<PlayerMovementManager>() != null) {
            _player.transform.parent = null;

        }
    }

    private void FixedUpdate() {
        if(Vector2.Distance(_platform.position, _placeObject) < 1 && _placeObject.x < _platform.position.x) {
            _dir = _placeOrigin - _placeObject;
        } else if(Vector2.Distance(_platform.position, _placeOrigin) < 1 && _placeOrigin.x < _platform.position.x) {
            _dir = _placeObject - _placeOrigin;
        }

        _platform.Translate(_speed * Time.fixedDeltaTime * _dir.normalized);

    }
}
