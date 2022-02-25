using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parametros
    [SerializeField]
    private int vida = 3;

    #endregion

    #region methods
    public void Damage()
    {
        vida--;

        if (vida <= 0)
        {

            Destroy(gameObject);
        }
    }
    #endregion
}
