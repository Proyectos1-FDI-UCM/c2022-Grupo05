using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    [SerializeField] private GameObject _volumeSlider;
    [SerializeField] private Sprite _controllerImage, _keyboardImage;
    [SerializeField] private Image _controlsImage;
    [SerializeField] private EventSystem _eventSys;



    [SerializeField] private AudioClip _clip;

    #endregion

    #region properties
    private bool _usingController;
    #endregion

    #region methods

    public void SetPause(bool usingController)
    {
        _pauseMenu.SetActive(true);
        NormalPause();

        Time.timeScale = 0;

        _usingController = usingController;
    }

    public void NormalPause()
    {
        SoundManager.Instance.PlayEffectSound(_clip);

        _normalPause.SetActive(true);
        _controlsPause.SetActive(false);
        _volumeSlider.SetActive(true);

        _normalReturnButton.onClick.AddListener(QuitPause);
        _mainMenuButton.onClick.AddListener(Instance.MainMenu);
        _controlsButton.onClick.AddListener(ControlsPause);

        _eventSys.SetSelectedGameObject(_controlsButton.gameObject);

    }

    private void MainMenu()
    {
        QuitPause();
        GameManager.Instance.MainMenu();
    }


    private void ControlsPause()
    {
        SoundManager.Instance.PlayEffectSound(_clip);

        _normalPause.SetActive(false);
        _controlsPause.SetActive(true);
        _controlsImage.sprite = _usingController ? _controllerImage : _keyboardImage;
        _volumeSlider.SetActive(false);

        _controlsReturnButton.onClick.AddListener(NormalPause);
        _eventSys.SetSelectedGameObject(_controlsReturnButton.gameObject);
    }


    public void QuitPause()
    {
        SoundManager.Instance.PlayEffectSound(_clip);

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
