
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region references
    [SerializeField] private Text _energyText, _levelText;
    [SerializeField] private GameObject _newGame, _loadGame;
    [SerializeField] private EventSystem _eventSys;
    [SerializeField]
    private GameObject _fade;
    private Animator _animator;
    #endregion

    #region methods
    public void AboutUs()
    {
        SceneManager.LoadScene("CREDITS");
    }

    public void LoadGame()
    {
        _animator.SetTrigger("Start");
        SoundManager.Instance.FadeAudio();

        Invoke("LoadScene", 0.3f);
    }

   
    public void NewGame()
    {
        _animator.SetTrigger("Start");
        SoundManager.Instance.FadeAudio();

        Invoke("NewScene", 0.3f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Nivel"));

        SoundManager.Instance.Level();
    }

    private void NewScene()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Nivel", "NIVEL 0");
        PlayerPrefs.SetInt("Energía", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("NIVEL 0");

        SoundManager.Instance.Level();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #region testing
    public void TestLevel0()
    {
        SceneManager.LoadScene("NIVEL 0");
        SoundManager.Instance.Level();
    }
    public void TestBoss()
    {
        SceneManager.LoadScene("NIVEL 3");
        SoundManager.Instance.Level();
    }

    #endregion

    #endregion

    private void Start()
    {
        SoundManager.Instance.MainMenu();

        if (PlayerPrefs.HasKey("Nivel"))
        {
            _eventSys.SetSelectedGameObject(_loadGame);
            _energyText.text = PlayerPrefs.GetInt("Energía").ToString();
            _levelText.text = PlayerPrefs.GetString("Nivel");
        }
        else 
        {
            _loadGame.SetActive(false);
            _newGame.transform.Translate(new Vector2(0, 1));
            _eventSys.SetSelectedGameObject(_newGame);
        }


        _animator = _fade.GetComponent<Animator>();

    }
}
