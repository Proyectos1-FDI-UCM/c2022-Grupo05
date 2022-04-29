using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerController : MonoBehaviour {
    #region references 
    private PlayerLifeComponent player;
    private EnemyLifeComponent _life;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision) {
        player = collision.GetComponent<PlayerLifeComponent>();

        if(player != null && player.AmpDash) {
            _life.Damage(false);
            GetComponentInParent<EnemyAnimation>().Damage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        player = collision.GetComponent<PlayerLifeComponent>();

        if(player != null) {
            player.Damage();
        }
    }


    private void Start() {
        _life = GetComponentInParent<EnemyLifeComponent>();
    }
    #endregion
}
