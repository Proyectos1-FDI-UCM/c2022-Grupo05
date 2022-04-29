using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagePlayerController : MonoBehaviour {
    #region references 
    private PlayerLifeComponent player;
    private BossLifeComponent _life;
    private BossAnimation _boss;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision) {
        player = collision.GetComponent<PlayerLifeComponent>();
        if(player != null && player.AmpDash) {
            _life.Damage(false);
            _boss.ActiveDamageAnimatio();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        player = collision.GetComponent<PlayerLifeComponent>();
        if(player != null) {
            player.Damage();
        }
    }


    private void Start() {
        _life = GetComponentInParent<BossLifeComponent>();
        _boss = GetComponentInParent<BossAnimation>();
    }
    #endregion
}
