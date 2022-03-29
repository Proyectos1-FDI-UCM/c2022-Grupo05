using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBossSecondPhase : MonoBehaviour
{
    private PlayerLifeComponent _player;
    [SerializeField]
    private BossSceneManager _scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<PlayerLifeComponent>();

        if (_player != null)
        {
            _scene.SetFase();

        }
    }
}
