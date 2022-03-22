using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseArea2 : MonoBehaviour
{
    private InputManager _player;
    private AudioClip _clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<InputManager>();

        if (_player != null)
        {
            ShakingCamera.Instance.ShakeCamera(5,1);
            SoundManager.Instance.PlaySound(_clip);

            Destroy(gameObject);
        }
    }

}
