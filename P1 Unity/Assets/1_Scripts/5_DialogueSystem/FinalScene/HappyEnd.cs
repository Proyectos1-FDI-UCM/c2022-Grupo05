using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyEnd : MonoBehaviour
{
    private InputManager _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<InputManager>();
        if (_player != null)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            CinematicManager.Instance.HappyEnd();
            Destroy(gameObject);
        }

    }
}
