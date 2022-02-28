using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InvertPlayerControlComponent : MonoBehaviour
{
    #region references 
    private InputManager player;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<InputManager>();
        if (player != null)
        {
            player.InvertControl();
            
        }
        
        Destroy(gameObject);
       // Debug.Log(collision.name);
    }
    #endregion
}
