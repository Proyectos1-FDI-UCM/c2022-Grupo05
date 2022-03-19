using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private float _timer = 0;
    private PlayerMovementManager _movement;
    private Checkpoint _checkpoint;
    private Checkpoint _miniCheckpoint;
    static private CheckpointManager _me;
    static public CheckpointManager Instance {
        get => _me;
    }

    public void GoToCheckpoint() {
        _checkpoint.GoToCheckpoint();
    }

    public void GoToMiniCheckpoint() {
        _miniCheckpoint.GoToCheckpoint();
    }

    public void NewCheckpoint(Checkpoint checkpoint) {
        _checkpoint = checkpoint;
    }

    public void NewMiniCheckpoint(Checkpoint checkpoint) {
        _miniCheckpoint = checkpoint;
    }

    private void Start() {
        _me = this;
        _movement = PlayerAccess.Instance.Movement;
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
