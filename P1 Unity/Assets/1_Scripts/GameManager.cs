using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region parameters
    private bool _dialogueOpen = false;
    #endregion


    #region properties
    private int _energy;
    private List<EnemyLifeComponent> _enemyList;
    private System.Random rnd = new System.Random();
    #endregion


    #region references
    static private GameManager _instance;
    static public GameManager Instance
    {
        get => _instance;
    }
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
        PlayerPrefs.SetInt("Energía", _energy);
        PlayerPrefs.SetString("Nivel", nextLevel);
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
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;


    }

    public float RNG(float minInclusive, float maxExclusive) {
        return (float)rnd.NextDouble() * (maxExclusive - minInclusive) + minInclusive;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _enemyList = new List<EnemyLifeComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
