using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseManager : MonoBehaviour
{

    #region references
 
    static private PauseManager _instance;
    static public PauseManager Instance
    { get { return _instance; } }


    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _normalPause;
    [SerializeField] private GameObject _controlsPause;
    [SerializeField] private Button _returnButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _controlsButton;

    #endregion

    #region methods
    public void PauseMenu()
    {
        if (Time.timeScale <= 0)
        {
            _pauseMenu.SetActive(true);
        }

        else
        {
            _pauseMenu.SetActive(false);
        }
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
