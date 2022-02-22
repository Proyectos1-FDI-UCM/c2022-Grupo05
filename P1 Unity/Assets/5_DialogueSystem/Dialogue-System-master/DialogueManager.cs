using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private Text nameText; //nombre del hablante
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Animator animator; //animación
    [SerializeField]
    private float TypingTime; //tiempo de tecleo de cada letra

    /// <summary>
    /// contiene los diálogos que va a aparecer en el text box en orden, carga nuevos sentences en la parte final del Queue a medida que el jugador va leyendo el texto
    /// </summary>
    private Queue<string> sentences;
    private Queue<string> speakers; //hablante

    //  initialización
    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<string>();
        enabled = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        enabled = true;
        sentences.Clear(); //borra texto de cualquier conversación previa

        foreach (string sentence in dialogue.sentences) //añade cada frase (sentence) al Queue para ser reproducida
        {

            sentences.Enqueue(sentence);
        }
        foreach (string speaker in dialogue.name) //añade en la cola los nombres de los hablantes en orden
        {

            speakers.Enqueue(speaker);
        }


        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) //si se han reproducido todas las frases, se acaba la conversación
        {
            enabled = false;
            EndDialogue();
            return;
        }

        Time.timeScale = 0f; //pausa la escena hasta terminar el diálogo
        string sentence = sentences.Dequeue();
        string speaker = speakers.Dequeue();
        StopAllCoroutines(); //limpiar corutinas anteriores
        StartCoroutine(TypeSentence(sentence, speaker)); //le pasa el nombre del hablante y la frase del diálogo para luego trocear en caracteres

    }


    public void SkipDialogue()
    {
        enabled = false;
        StopAllCoroutines(); //limpiar corutinas anteriores
        EndDialogue();

    }


    /// <summary>
    ///escribe en el text box la línea de diálogo correspondiente
    /// </summary>
    IEnumerator TypeSentence(string sentence, string speaker)
    {
        dialogueText.text = "";
        nameText.text = speaker;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(TypingTime); //el diálogo ignora la pausa y seguirá escribiendo con normalidad
        }
    }



    /// <summary>
    /// Acaba la conversación
    /// </summary>
    void EndDialogue()
    {
        Time.timeScale = 1f; //resume la escena
      
        animator.SetBool("IsOpen", false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //espacio o botón izq del ratón
            DisplayNextSentence();
    }

}
