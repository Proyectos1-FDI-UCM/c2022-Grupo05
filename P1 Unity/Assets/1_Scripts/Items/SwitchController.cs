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
            SoundManager.Instance.PlayEffectSound(_clip);
            //ShakingCamera.Instance.ShakeCamera(2,0.7f);
            Destroy(gameObject);
        }
    }

    #endregion
    
}
