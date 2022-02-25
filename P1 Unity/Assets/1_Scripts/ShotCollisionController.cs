using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotCollisionController : MonoBehaviour
{
    #region references 
    private EnemyLifeComponent enemy;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<EnemyLifeComponent>();
        if (enemy != null)
        {
            enemy.Damage();
        }
        Destroy(gameObject);
        Debug.Log("hola");
    }
    #endregion
}
