using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCollision : MonoBehaviour
{
    public UnityAction accionEntrar;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerLifeComponent player;
        player = collider.GetComponent<PlayerLifeComponent>();
        if (player == null)
        {
            accionEntrar();
        }
       
    }
   
}
