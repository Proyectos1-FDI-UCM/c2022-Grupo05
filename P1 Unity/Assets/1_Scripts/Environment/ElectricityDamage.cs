using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityDamage : MonoBehaviour
{
    #region references 
    private PlayerLifeComponent _playerLife;

    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerLife = collision.GetComponent<PlayerLifeComponent>();

        if (_playerLife == null) _playerLife = collision.GetComponentInParent<PlayerLifeComponent>();
        if (_playerLife != null)
        {
            _playerLife.Damage();
        }
    }

    #endregion
}