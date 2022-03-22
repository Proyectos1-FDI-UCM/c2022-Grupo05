using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private bool resumeShot = true;

    #region references
    [SerializeField]
    private Text nameText; //nombre del hablante
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Animator animator; //animaci�n
    [SerializeField]
    private float TypingTime; //tiempo de tecleo de cada letra
    
    #endregion

    #region singleton
    static private DialogueManager _instance;
    static public DialogueManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    #endregion

    /// <summary>
    /// contiene los di�logos que va a aparecer en el text box en orden, carga nuevos sentences en la parte final del Queue a medida que el jugador va leyendo el texto
    /// </summary>
    private Queue<string> sentences;
    private Queue<string> speakers; //hablante

    //  initializaci�n
    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<string>();
        enabled = false;
        resumeShot = true;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        PlayerAccess.Instance.Input.HasShot(false);
        // Impide activar la pausa
        GameManager.Instance.DialogueOpened(true);

        animator.SetBool("IsOpen", true);

        enabled = true;
        sentences.Clear(); //borra texto de cualquier conversaci�n previa

        foreach (string sentence in dialogue.sentences) //a�ade cada frase (sentence) al Queue para ser reproducida
        {

            sentences.Enqueue(sentence);
        }
        foreach (string speaker in dialogue.name) //a�ade en la cola los nombres de los hablantes en orden
        {

            speakers.Enqueue(speaker);
        }


        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) //si se han reproducido todas las frases, se acaba la conversaci�n
        {
            enabled = false;

            EndDialogue();
            return;
        }

        Time.timeScale = 0f; //pausa la escena hasta terminar el di�logo
        string sentence = sentences.Dequeue();
        string speaker = speakers.Dequeue();
        StopAllCoroutines(); //limpiar corutinas anteriores
        StartCoroutine(TypeSentence(sentence, speaker)); //le pasa el nombre del hablante y la frase del di�logo para luego trocear en caracteres

    }

    public void SkipDialogue()
    {
        Debug.Log("Skip");
        enabled = false;
        StopAllCoroutines(); //limpiar corutinas anteriores
        EndDialogue();

    }

    /// <summary>
    ///escribe en el text box la l�nea de di�logo correspondiente
    /// </summary>
    IEnumerator TypeSentence(string sentence, string speaker)
    {
        dialogueText.text = "";
        nameText.text = speaker;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(TypingTime); //el di�logo ignora la pausa y seguir� escribiendo con normalidad
        }
    }

    /// <summary>
    /// Acaba la conversaci�n
    /// </summary>
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        // Activa de nuevo la pausa
        GameManager.Instance.DialogueOpened(false);
        StartCoroutine(ResumeGame());

    }

    public IEnumerator ResumeGame()
    {

        Time.timeScale = 1f; //resume la escena
                             // Activa de nuevo la pausa
        GameManager.Instance.DialogueOpened(false);

        yield return new WaitForSecondsRealtime(1.0f);
        if (resumeShot)
           PlayerAccess.Instance.Input.HasShot(true);


    }

    public void DesactivateShot ()
    {
        resumeShot = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //espacio o boton izq del raton
            DisplayNextSentence();
    }

}
