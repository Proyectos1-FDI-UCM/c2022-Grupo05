using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void OnPlayerDeath()
    {
        
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
