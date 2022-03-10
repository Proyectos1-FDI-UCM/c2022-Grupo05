using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    #region references
    static private HUDController _instance;
    static public HUDController Instance
    {
        get => _instance;
    }

    [SerializeField] private Transform _hudElements;
    [SerializeField] private Sprite[] _hpBars;
    [SerializeField] private Sprite[] _energyBars;

    //Sistema de vida Boss
    [SerializeField] private GameObject[] _hpBoss;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _energyBar;
    [SerializeField] private GameObject _bossBar;
    [SerializeField] private Text _energyShards;
    [SerializeField] private GameObject _gameOverText;

    #endregion


    #region methods
    public void UpdatePlayerHP(int life)
    {
        _hpBar.sprite = _hpBars[life];
    }

    public void UpdateBossHP(int life)
    {
        _hpBoss[life].SetActive(false) ;
    }


    public void UpdateEnergy(int energy)
    {
        _energyBar.sprite = _energyBars[energy];
    }

    public void UpdateShards(int shards)
    {
        _energyShards.text = shards.ToString("D3");
    }

    public void ChangePosition(bool changed)
    {
        _hudElements.Translate(new Vector2(0, changed ? -900 : 900));
    }

    public void ShowBossBar(bool value)
    {
        _bossBar.SetActive (value);
    }

    public void ShowGameOverText(bool value)
    {
        _gameOverText.SetActive(value);
    }

    private void Awake()
    {
        _instance = this;

    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        ShowBossBar(false);
    }
}
