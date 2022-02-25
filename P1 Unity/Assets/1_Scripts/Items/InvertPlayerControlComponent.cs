using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InvertPlayerControlComponent : MonoBehaviour
{
    #region references 
    private InputManager1 player;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<InputManager1>();
        if (player != null)
        {
            player.InvertControl();
            
        }
        

        Destroy(gameObject);
        Debug.Log(collision.name);
    }
    #endregion
}
