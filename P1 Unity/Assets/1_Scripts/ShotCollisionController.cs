using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCollisionController : MonoBehaviour
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
            Debug.Log("Enemy damaged");
        }
       
        Destroy(gameObject);
    }
    #endregion
}
