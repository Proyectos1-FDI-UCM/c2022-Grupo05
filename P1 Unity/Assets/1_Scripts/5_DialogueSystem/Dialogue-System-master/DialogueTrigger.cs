using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//se encarga de enviar los diálogos al DialogueManager (DM) al pulsar un botón o al entrar en una zona (on Trigger)
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
