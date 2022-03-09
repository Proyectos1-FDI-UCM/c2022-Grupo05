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

    public void SetPause()
    {
        _pauseMenu.SetActive(true);
        NormalPause();

        Time.timeScale = 0;
    }

    public void NormalPause()
    {
        _normalPause.SetActive(true);
        _controlsPause.SetActive(false);

        _normalReturnButton.onClick.AddListener(QuitPause);
        _mainMenuButton.onClick.AddListener(GameManager.Instance.MainMenu);
        _controlsButton.onClick.AddListener(ControlsPause);

    }

    public void ControlsPause()
    {
        _normalPause.SetActive(false);
        _controlsPause.SetActive(true);

        _controlsReturnButton.onClick.AddListener(NormalPause);
    }


    public void QuitPause()
    {
        _pauseMenu.SetActive(false);
        _normalPause.SetActive(false);
        _controlsPause.SetActive(false);
        Time.timeScale = 1;

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
