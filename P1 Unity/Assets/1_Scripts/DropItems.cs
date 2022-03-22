using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    #region properties
    [SerializeField]
    private GameObject p_health;

    [SerializeField]
    private GameObject p_recharge;
    #endregion


    private int _rnd;

    public void DropItem()
    {
        float _rnd = GameManager.Instance.RNG(0, 5);

        if (_rnd < 1)
        {
            GameObject.Instantiate(p_health, _myTransform.position, _myTransform.rotation);
        }

        else if (_rnd < 2)
        {
            GameObject.Instantiate(p_recharge, _myTransform.position, _myTransform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }
}
