using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigravityStoneController : MonoBehaviour
{
    #region references
    private PlayerMovementManager player;
    #endregion
    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.GetComponent<PlayerMovementManager>();
        if (player != null)
        {
            player.EnableAntigravity(true);
        }
    }
}
