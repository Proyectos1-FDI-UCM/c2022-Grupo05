using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private bool _isMiniCheckpoint = true;
    private Transform _transform;
    private PlayerMovementManager _movement;
    [SerializeField]
    private bool _isGravityChanged;
    [SerializeField]
    private bool _forceGravity;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<PlayerMovementManager>() != null) {
            if(_isMiniCheckpoint) CheckpointManager.Instance.NewMiniCheckpoint(this);
            else CheckpointManager.Instance.NewCheckpoint(this);
            if(!_forceGravity) _isGravityChanged = _movement.IsGravityChanged;
        }
    }

    public void GoToCheckpoint() {
        if(_isGravityChanged != _movement.IsGravityChanged) _movement.ForceChangeGravity();
        PlayerAccess.Instance.Transform.position = _transform.position;
        PlayerAccess.Instance.Rigidbody.velocity = Vector2.zero;
        _movement.enabled = false;
        _movement.StopDashing();
    }

    private void Start() {
        _transform = transform;
        _movement = PlayerAccess.Instance.Movement;
    }
}
