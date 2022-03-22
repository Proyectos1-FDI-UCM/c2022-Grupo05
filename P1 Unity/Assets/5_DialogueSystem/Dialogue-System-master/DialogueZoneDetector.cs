using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//se encarga de detectar si el jugador entra en la zona donde se quiera activar diálogo mediante OnTrigger, y llamará al Componente DialogueTrigger
public class DialogueZoneDetector : MonoBehaviour
{
    private InputManager _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<InputManager>();
       if (_player!= null)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            Destroy(gameObject);
        }
        
    }
   
}
