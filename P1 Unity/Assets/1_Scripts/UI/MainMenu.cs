
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region references
    [SerializeField] private Text _energyText, _levelText;
    [SerializeField] private GameObject _newGame, _loadGame;
    #endregion

    #region methods
    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Nivel"));
    }

    public void NewGame() 
    {
        PlayerPrefs.SetString("Nivel", "NIVEL 0");
        PlayerPrefs.SetInt("Energía", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("NIVEL 0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("Nivel"))
        {
            _energyText.text = PlayerPrefs.GetInt("Energía").ToString();
            _levelText.text = PlayerPrefs.GetString("Nivel");
        }
        else 
        {
            _loadGame.SetActive(false);
            _newGame.transform.Translate(new Vector2(0, 1)); ;
        }
    }
}
