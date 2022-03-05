using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parametros
    private bool _damage;
    [SerializeField]
    private int vidaMaxima;
    #endregion

    #region properties
      private int vida;
    #endregion

    #region references
    private DropItems _dropItem;
    [SerializeField]
    private GameObject _dust;
    private Transform _myTransfrom;
    #endregion

    #region methods
    public void Damage()
    {
        vida--;
        _damage = true;
        if (vida <= 0)
        {
            GameManager.Instance.OnEnemyDeath(this);
            _dropItem.DropItem();
            gameObject.SetActive(false);
            Instantiate(_dust,_myTransfrom.position,Quaternion.identity);
        }
    }

    public void Respawn() 
    {
        gameObject.SetActive(true);
        vida = vidaMaxima;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMaxima;
        _dropItem = gameObject.GetComponent<DropItems>();
        _myTransfrom = transform;
    }
}
