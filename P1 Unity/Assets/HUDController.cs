using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    #region references
    [SerializeField] private Sprite[] _hpBars;
    [SerializeField] private Sprite[] _energyBars;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _energyBar;
    [SerializeField] private Image _bossBar;
    [SerializeField] private Text _energyShards;
    #endregion


    #region methods
    public void UpdateHP(int life) 
    {
        _hpBar.sprite = _hpBars[life];
    }

    public void UpdateEnergy(int energy)
    {
        _energyBar.sprite = _energyBars[energy];
    }

    public void UpdateShards(int shards)
    {
        _energyShards.text = shards.ToString("D3");
    }


    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }
}
