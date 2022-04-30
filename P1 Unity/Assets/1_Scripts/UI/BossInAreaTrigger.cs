using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInAreaTrigger : MonoBehaviour
{
    #region references 
    private InputManager player;
    [SerializeField] private HUDController _hudController;
    [SerializeField] private GameObject _bossObject;

    [SerializeField] private GameObject[] _items;

    #endregion

    #region methods

    public void SpawnBoss()
    {
        StartCoroutine(SpawnBossCoroutine());
    }

    IEnumerator SpawnBossCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        foreach (GameObject _item in _items)
        {
            if (_item != null)
            {
                _item.SetActive(false);
            }
        }
        _bossObject.SetActive(true);
        _hudController.ShowBossBar(true);
    }

    #endregion
}
