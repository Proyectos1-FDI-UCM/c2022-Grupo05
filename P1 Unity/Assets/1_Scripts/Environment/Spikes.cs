using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerLifeComponent playerLife = collision.GetComponent<PlayerLifeComponent>();
        if(playerLife == null) playerLife = collision.GetComponentInParent<PlayerLifeComponent>();
        if(playerLife != null) {
            playerLife.Damage();
            CheckpointManager.Instance.GoToMiniCheckpoint();
        }
    }
}
