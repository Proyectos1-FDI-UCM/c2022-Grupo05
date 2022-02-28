using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region references 
    private Animator animator;
    private ShotMovementController _pShot;
    #endregion

    #region methods
    private void Deactivate()
    {
        animator.SetBool("damaged", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _pShot = collision.GetComponent<ShotMovementController>();
        if (_pShot != null)
        {    
            animator.SetBool("damaged", true);
        }
        Invoke("Deactivate", 0.01f);
       
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("Deactivate", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
