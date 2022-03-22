using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//se encarga de enviar los di�logos al DialogueManager (DM) al pulsar un bot�n o al entrar en una zona (on Trigger)
public class DialogueTrigger : MonoBehaviour 
{
	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		DialogueManager.Instance.StartDialogue(dialogue);
	}

}
