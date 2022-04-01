using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotCollision : MonoBehaviour
{
    private PlayerLifeComponent _player;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<PlayerLifeComponent>();

        if (_player != null)
        {
            _player.Damage();
            Destroy(gameObject);
        }
    }
}
