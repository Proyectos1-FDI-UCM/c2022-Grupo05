using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagePlayerController : MonoBehaviour
{
    #region references 
    private PlayerLifeComponent player;
    private BossLifeComponent _life;
    #endregion

    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerLifeComponent>();

        if (player != null)
        {
            player.Damage();
            if (player.AmpDash)
            {
                _life.Damage(false);
            }
        }
    }


    private void Start()
    {
        _life = GetComponentInParent<BossLifeComponent>();
    }
    #endregion
}
