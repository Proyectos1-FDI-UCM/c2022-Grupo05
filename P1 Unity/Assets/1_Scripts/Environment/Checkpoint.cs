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
        if(_isMiniCheckpoint) CheckpointManager.Instance.NewMiniCheckpoint(this, out _movement);
        else CheckpointManager.Instance.NewCheckpoint(this, out _movement);
        if(!_forceGravity) _isGravityChanged = _movement.IsGravityChanged;
    }

    public void GoToCheckpoint(Transform playerTransform, Rigidbody2D rigidbody) {
        if(_isGravityChanged != _movement.IsGravityChanged) _movement.ForceChangeGravity();
        playerTransform.position = _transform.position;
        rigidbody.velocity = Vector2.zero;
        _movement.enabled = false;
        _movement.StopDashing();
    }

    private void Start() {
        _transform = transform;
    }
}
