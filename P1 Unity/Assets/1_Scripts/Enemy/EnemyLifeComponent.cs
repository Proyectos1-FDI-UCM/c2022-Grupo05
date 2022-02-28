using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parametros
    private bool _damage;
    [SerializeField]
    private int vida = 3;
    #endregion

    #region references
    private DropItems _dropItem;
    #endregion

    #region methods
    public void Damage()
    {
        vida--;
        _damage = true;
        if (vida <= 0)
        {
            _dropItem.DropItem();
            Destroy(gameObject);
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _dropItem = gameObject.GetComponent<DropItems>();
    }
}
