using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region parameters
    private bool _dialogueOpen = false;
    [SerializeField] private bool _sendPlayerPosition=true;
    #endregion


    #region properties
    private int _energy;
    private List<EnemyLifeComponent> _enemyList;
    private System.Random rnd = new System.Random();
    [SerializeField] private Vector2 _playerStartPosition;
    #endregion


    #region references
    static private GameManager _instance;
    static public GameManager Instance
    {
        get => _instance;
    }

    [SerializeField]
    private GameObject _fade;
    private Animator _animator;
    #endregion


    #region methods
    public void OnPlayerDeath(GameObject player)
    {
        player.SetActive(false); 
        HUDController.Instance.ShowGameOverText(true);
        StartCoroutine(RestarLevel(player));
    }

    public void OnEnemyDeath(EnemyLifeComponent enemy) 
    {
        _enemyList.Add(enemy);
    }

    private IEnumerator RestarLevel(GameObject player) 
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.anyKeyDown);

        _animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("End");

        foreach (EnemyLifeComponent enemy in _enemyList) enemy.Respawn();
        _enemyList = new List<EnemyLifeComponent>();
        HUDController.Instance.ShowGameOverText(false);
        CheckpointManager.Instance.GoToCheckpoint();
        PlayerAccess.Instance.Life.Heal(5);
        PlayerAccess.Instance.Life.GetEnergy(3);

        player.SetActive(true);
    }

    public void AddEnergy(int energy)
    {
        _energy += energy;
        HUDController.Instance.UpdateShards(_energy);
    }
    public int CountEnergy()
    {
        return _energy;
    } 

    public void NextLevel(string nextLevel)
    {
        StartCoroutine(LoadNextLevel(nextLevel));
    }


    IEnumerator LoadNextLevel(string nextLevel)
    {
        _animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        PlayerPrefs.SetInt("Energía", _energy);
        PlayerPrefs.SetString("Nivel", nextLevel);
        PlayerAccess.Instance.Life.SavePlayer();
        PlayerPrefs.Save();
        SceneManager.LoadScene(nextLevel);
    }


    private void Awake()
    {
        _energy = PlayerPrefs.GetInt("Energía");

        _instance = this;
    }


    public void DialogueOpened(bool isOpened)
    {
        _dialogueOpen = isOpened;
    }

    public void PauseMenu()
    {
        if (!_dialogueOpen)
        {
            if (Time.timeScale <= 0)
            {
                PauseManager.Instance.QuitPause();
            }

            else
            {
                PauseManager.Instance.SetPause();
            }
        }

    }


    public void MainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        SoundManager.Instance.FadeAudio();

        _animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;

        SoundManager.Instance.MainMenu();
    }



    public float RNG(float minInclusive, float maxExclusive) {
        return (float)rnd.NextDouble() * (maxExclusive - minInclusive) + minInclusive;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _enemyList = new List<EnemyLifeComponent>();
        HUDController.Instance.UpdateShards(_energy);
        if(PlayerPrefs.HasKey("Vida") && PlayerPrefs.HasKey("Carga"))
            PlayerAccess.Instance.Life.SetPlayer(PlayerPrefs.GetInt("Vida"), PlayerPrefs.GetInt("Carga"));
        if (_sendPlayerPosition)
         PlayerAccess.Instance.Transform.position = _playerStartPosition;


        _animator = _fade.GetComponent<Animator>();
    }


}
