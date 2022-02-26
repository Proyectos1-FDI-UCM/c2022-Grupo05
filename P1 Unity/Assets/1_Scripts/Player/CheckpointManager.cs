using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private float _timer = 0;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private PlayerMovementManager _movement;
    private Checkpoint _checkpoint;
    private Checkpoint _miniCheckpoint;
    static private CheckpointManager _me;
    static public CheckpointManager Instance {
        get => _me;
    }


    public void GoToCheckpoint() {
        _checkpoint.GoToCheckpoint(_transform, _rigidbody);
    }

    public void GoToMiniCheckpoint() {
        _miniCheckpoint.GoToCheckpoint(_transform, _rigidbody);
    }

    public void NewCheckpoint(Checkpoint checkpoint, out PlayerMovementManager movement) {
        _checkpoint = checkpoint;
        movement = _movement;
    }

    public void NewMiniCheckpoint(Checkpoint checkpoint, out PlayerMovementManager movement) {
        _miniCheckpoint = checkpoint;
        movement = _movement;
    }

    private void Start() {
        _me = this;
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovementManager>();
    }

    private void Update() {
        if(!_movement.enabled) {
            _timer += Time.deltaTime;
            if(_timer > 0.25f) {
                _movement.enabled = true;
                _timer = 0;
            }
        }
    }
}
