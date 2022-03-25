using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndTrigger : MonoBehaviour
{
    private InputManager _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<InputManager>();
        if (_player != null)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            CinematicManager.Instance.BadEnd();
            Destroy(gameObject);
        }

    }
}
