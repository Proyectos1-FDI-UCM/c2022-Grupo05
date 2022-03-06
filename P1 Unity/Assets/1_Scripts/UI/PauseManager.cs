using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PauseManager : MonoBehaviour
{

    #region references
 
    static private PauseManager _instance;
    static public PauseManager Instance
    { get { return _instance; } }


    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _normalPause;
    [SerializeField] private GameObject _controlsPause;
    [SerializeField] private Button _normalReturnButton;
    [SerializeField] private Button _controlsReturnButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _controlsButton;

    #endregion


    #region methods
    public void PauseMenu()
    {
        if (Time.timeScale <= 0)
        {
            _pauseMenu.SetActive(true);
            _normalPause.SetActive(true);
            _controlsPause.SetActive(false);
        }

        else
        {
            _pauseMenu.SetActive(false);
        }

        _normalReturnButton.onClick.AddListener(GameManager.Instance.PauseMenu);
        _mainMenuButton.onClick.AddListener(GameManager.Instance.MainMenu);
        _controlsButton.onClick.AddListener(ControlsMenu);
    }

    public void ControlsMenu()
    {
        _normalPause.SetActive(false);
        _controlsPause.SetActive(true);
        _controlsReturnButton.onClick.AddListener(PauseMenu);
    }


    #endregion


    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.SetActive(false);
    }

}
