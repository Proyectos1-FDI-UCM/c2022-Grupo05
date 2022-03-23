using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    #region parameters
    [SerializeField] private string _nextLevel;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerLifeComponent>()) GameManager.Instance.NextLevel(_nextLevel);
    }


}
