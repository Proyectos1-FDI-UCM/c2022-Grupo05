using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//se encarga de enviar los diálogos al DialogueManager (DM) al pulsar un botón o al entrar en una zona (on Trigger)
public class DialogueTrigger : MonoBehaviour 
{
	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		DialogueManager.Instance.StartDialogue(dialogue);
	}

}
