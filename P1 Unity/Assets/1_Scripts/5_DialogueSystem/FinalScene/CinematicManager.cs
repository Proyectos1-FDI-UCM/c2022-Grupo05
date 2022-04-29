using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{

    [SerializeField] private Vector3 _playerPosition1;
    [SerializeField] private Vector3 _playerPosition2;

    #region references
    [SerializeField] private int minEnergy = 60;
    [SerializeField] private GameObject _playerAnim;
    [SerializeField] private GameObject _sendStoneAnim;
    [SerializeField] private GameObject _sentStoneDialogue;
    [SerializeField] private GameObject _pointLight;

    [SerializeField] private GameObject _happyEndTrigger;
    [SerializeField] private GameObject _badEndTrigger;
    [SerializeField] private GameObject _happyEndAnim;
    [SerializeField] private GameObject _badEndAnim;

    [SerializeField] private AudioClip _navEscape;


    [SerializeField] private GameObject _skip;

    static private CinematicManager _instance;
    static public CinematicManager Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        _instance = this;
    }


    #endregion
    void Start()
    {
        _sendStoneAnim.SetActive(false);
        _pointLight.SetActive(true);
        _playerAnim.SetActive(false);

        _happyEndTrigger.SetActive(false);
        _badEndTrigger.SetActive(false);

        _happyEndAnim.SetActive(false);
        _badEndAnim.SetActive(false);

        _skip.SetActive(false);
        PlayerAccess.Instance.Input.HasShot(false);
        DialogueManager.Instance.DesactivateShot();



    }

    public void DesactivateGravity()
    {
        PlayerAccess.Instance.Movement.EnableAntigravity(false);
    }

    public void SendStoneAnim()
    {
        _skip.SetActive(true);
        PlayerAccess.Instance.gameObject.SetActive(false);

        _sendStoneAnim.SetActive(true);
        _pointLight.SetActive(false);
    }

    public void EndSentStoneAnim()
    {
       
        _skip.SetActive(false);
        _sentStoneDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
        _sendStoneAnim.SetActive(false);
        PlayerAccess.Instance.gameObject.SetActive(true);
        PlayerAccess.Instance.Transform.transform.position = _playerPosition1;

        SoundManager.Instance.PlayCinematicSound(_navEscape);


        Branch();

    }



    private void Branch()
    {
        if (GameManager.Instance.CountEnergy() >= minEnergy)
        {
            _happyEndTrigger.SetActive(true);
            _badEndTrigger.SetActive(false);
        }
        else
        {
            _badEndTrigger.SetActive(true);
            _happyEndTrigger.SetActive(false);
        }

    }
    public void BadEnd()
    {
        PlayerAccess.Instance.Transform.transform.position = _playerPosition2;
        PlayerAccess.Instance.gameObject.SetActive(false);
        _badEndAnim.SetActive(true);
    }

    public void HappyEnd()
    {
        PlayerAccess.Instance.Transform.transform.position = _playerPosition2;
        PlayerAccess.Instance.gameObject.SetActive(false);
        _happyEndAnim.SetActive(true);
    }


    public void SkipDialogue()
    {

        EndSentStoneAnim();

    }

    public void FinJuego()
    {
        StartCoroutine(ReturnMenu());
        
    }
    IEnumerator ReturnMenu()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main Menu");
    }
}
