using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    #region references 
    private Animator animator;
   // private ShotMovementController _pShot;
    #endregion
    #region methods
    public void DeactivateDamagerE()
    {
        animator.SetBool("damaged", false);
    }
    public void ActiveDamageAnimatio()
    {
        if (transform.parent != null)
        {
            animator.SetBool("damaged", true);
        }
        
    }
  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        _pShot = collision.GetComponent<ShotMovementController>();
        if (_pShot != null)
        {
            ActiveDamageAnimatio();
        }
    }*/
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //  animator=GameObject.Find("")
        animator = GetComponent<Animator>();
    }
    
}
