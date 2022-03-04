using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//se encarga de detectar si el jugador entra en la zona donde se quiera activar diálogo mediante OnTrigger, y llamará al Componente DialogueTrigger
public class DialogueZoneDetector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       if (collision.GetComponent<PlayerMovementManager>() != null) //(CAMBIAR NOMBRE COMPONENTE)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            Destroy(gameObject);
        }
        
    }
   
}
