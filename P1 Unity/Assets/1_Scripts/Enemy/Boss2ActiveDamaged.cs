using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2ActiveDamaged : MonoBehaviour
{
    private BossAnimation _boss;
    private ShotCollisionController _shot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _shot =collision.GetComponent<ShotCollisionController>();
        if (_shot !=null)
        {
            _boss.ActiveDamageAnimatio();

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _boss = GetComponentInParent<BossAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
