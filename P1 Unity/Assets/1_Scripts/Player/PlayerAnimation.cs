using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region references 
    private Animator animator;
    private DamagePlayerController _enemy;
    #endregion
    #region methods
    
    public void OffDamager()
    {
        animator.SetBool("_damage", false);
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemy = collision.GetComponent<DamagePlayerController>();
        if(_enemy!=null||collision.gameObject.layer==8)
        {
            animator.SetTrigger("enemy");
        }
       // Invoke("Damager", 1f);
    }*/
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

}
