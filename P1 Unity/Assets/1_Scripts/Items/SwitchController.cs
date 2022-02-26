using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    #region refenrece
    private ShotCollisionController _playerShot;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerShot = collision.GetComponent<ShotCollisionController>();
        if (_playerShot!=null)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
}
