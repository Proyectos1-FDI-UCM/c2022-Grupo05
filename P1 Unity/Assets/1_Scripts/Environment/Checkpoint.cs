using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private bool _isMiniCheckpoint = true;
    private Transform _transform;
    private PlayerMovementManager _movement;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(_isMiniCheckpoint) CheckpointManager.Instance.MiniCheckpoint = this;
        else CheckpointManager.Instance.Checkpoint = this;
    }

    public void GoToCheckpoint(Transform playerTransform, Rigidbody2D rigidbody, PlayerMovementManager movement) {
        playerTransform.position = _transform.position;
        rigidbody.velocity = Vector2.zero;
        movement.enabled = false;
    }

    private void Start() {
        _transform = transform;
    }
}
