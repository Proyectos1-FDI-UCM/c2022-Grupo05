using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyDetection : MonoBehaviour
{
    #region references 
    PlayerMovementManager player;
    #endregion
    public UnityAction accionEntrar, accionSalir;

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)   
    {
        player = collision.GetComponent<PlayerMovementManager>();
        if (player != null)
        {
            //Debug.Log("player");
            accionEntrar();
        }
    }
    private void OnTriggerExit2D(Collider2D p)
    {
        player = p.GetComponent<PlayerMovementManager>();
        if (player != null)
        {
            //Debug.Log("exit");          
            accionSalir();
        }
    }

    #endregion

}
