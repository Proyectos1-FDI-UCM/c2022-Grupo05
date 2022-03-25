using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//se encarga de enviar los di�logos al DialogueManager (DM) al pulsar un bot�n o al entrar en una zona (on Trigger)
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] private AudioClip _dialogueClip;
    public void TriggerDialogue()
    {
        if (_dialogueClip != null)
            SoundManager.Instance.PlayCinematicSound(_dialogueClip);

        DialogueManager.Instance.StartDialogue(dialogue);
    }

}
