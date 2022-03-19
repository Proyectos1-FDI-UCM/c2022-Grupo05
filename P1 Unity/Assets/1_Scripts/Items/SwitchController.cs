using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    #region refenrece
    private ShotCollisionController _playerShot;

    [SerializeField] private AudioClip _clip;

    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerShot = collision.GetComponent<ShotCollisionController>();
        if (_playerShot!=null)
        {
            SoundManager.Instance.PlaySound(_clip);
            Destroy(gameObject);
        }
    }

    #endregion
    
}
