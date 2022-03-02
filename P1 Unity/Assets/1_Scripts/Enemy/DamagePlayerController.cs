using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerController : MonoBehaviour
{
    #region references 
    private PlayerLifeComponent player;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerLifeComponent>();

        if (player != null)
        {
            player.Damage();
            Debug.Log("Damage the player by " + gameObject);
        }
    }
    #endregion
}
