using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region references 
    private Animator animator;
  
    #endregion
    #region methods
    
    public void OffDamager()
    {
        animator.SetBool("_damage", false);
    }
 
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

}
