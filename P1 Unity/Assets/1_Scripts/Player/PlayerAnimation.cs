using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region references 
    private Animator animator;
    #endregion

    #region methods
    void Start() {
        animator = GetComponent<Animator>();
    }

    public void OffDamager() {
        animator.SetBool("_damage", false);
    }

    public void OffShot() {
        animator.SetBool("_shot", false);
    }

    public void Run(bool activate) {
        animator.SetBool("run", activate);
    }

    public void Jump(int numJump) {
        switch(numJump) {
            case 0:
                animator.SetBool("_jump", false);
                animator.SetBool("_jump2", false);
                break;
            case 1:
                animator.SetBool("_jump", true);
                break;
            case 2:
                animator.SetBool("_jump", true);
                animator.SetBool("_jump2", true);
                break;
        }
    }
    #endregion
}
