using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{

    [SerializeField] private Vector3 _playerPosition1;
    [SerializeField] private Vector3 _playerPosition2;
    private Animator _animator;

    #region references
    [SerializeField] private int minEnergy = 60;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerAnim;
    [SerializeField] private GameObject _sendStoneAnim;
    [SerializeField] private GameObject _pointLight;
    [SerializeField] private GameObject _machine;
    [SerializeField] private GameObject _happyEndTrigger;
    [SerializeField] private GameObject _badEndTrigger;
    [SerializeField] private GameObject _happyEndAnim;
    [SerializeField] private GameObject _badEndAnim;

    [SerializeField] private GameObject _sentStoneDialogue;

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
       
        _machine.SetActive(false);
        _happyEndTrigger.SetActive(false);
        _badEndTrigger.SetActive(false);

        _happyEndAnim.SetActive(false);
        _badEndAnim.SetActive(false);

        _player.GetComponent<InputManager>().HasShot(false);

    }

    public void DesactivateGravity()
    {
       _player.GetComponent<PlayerMovementManager>().HasAntigravity(false);
    }

    public void SendStoneAnim ()
    {
        _player.SetActive(false);
        _sendStoneAnim.SetActive(true);
        _pointLight.SetActive(false);
    }

    public void EndSentStoneAnim()
    {
        _sentStoneDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
        _sendStoneAnim.SetActive(false);
        _player.SetActive(true);
        _player.transform.position = _playerPosition1;
        _machine.SetActive(true);
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
        _player.transform.position = _playerPosition2;
        _player.SetActive(false);
        _badEndAnim.SetActive(true);
    }

    public void HappyEnd()
    {
        _player.transform.position = _playerPosition2;
        _player.SetActive(false);
        _happyEndAnim.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}