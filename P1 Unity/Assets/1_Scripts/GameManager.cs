using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region parameters
    #endregion


    #region properties
    private int _energy;
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
        Destroy(player); 
        HUDController.Instance.ShowGameOverText();
        StartCoroutine(RestarLevel());

    }

    public IEnumerator RestarLevel() 
    {
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene("");

    }

    public void AddEnergy(int energy)
    {
        _energy += energy;
        HUDController.Instance.UpdateShards(_energy);
    }

    private void Awake()
    {
        _instance = this;
    }

    public void PauseMenu()
    {
        if (Time.timeScale <= 0)
        {
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0;
        }

        PauseManager.Instance.PauseMenu();
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _energy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
