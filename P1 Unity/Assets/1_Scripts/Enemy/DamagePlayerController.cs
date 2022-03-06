using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerController : MonoBehaviour
{
    #region references 
    private PlayerLifeComponent player;
    private EnemyLifeComponent _life;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerLifeComponent>();

        if (player != null)
        {
            if(!player.Damage()) { // Hacer daño al jugador, si no hace daño se ejecuta el bloque de comandos
                _life.Damage(false);
            }
        }
    }

    private void Start() {
        _life = GetComponentInParent<EnemyLifeComponent>();
    }
    #endregion
}
