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
    public Checkpoint Checkpoint {
        set => _checkpoint = value;
    }
    public Checkpoint MiniCheckpoint {
        set => _miniCheckpoint = value;
    }
    static private CheckpointManager _me;
    static public CheckpointManager Instance {
        get => _me;
    }


    public void GoToCheckpoint() {
        _checkpoint.GoToCheckpoint(_transform, _rigidbody, _movement);
    }

    public void GoToMiniCheckpoint() {
        _miniCheckpoint.GoToCheckpoint(_transform, _rigidbody, _movement);
    }

    void Start() {
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
