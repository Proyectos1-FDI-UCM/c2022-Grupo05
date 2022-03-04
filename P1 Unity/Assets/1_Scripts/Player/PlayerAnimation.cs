using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region references 
    private Animator animator;
    #endregion
    #region methods
    public void Run(bool c)
    {
        animator.SetBool("run", c);
    }
    public void Jump(bool c)
    {
        animator.SetBool("_jump", c);
    }
    public void Shot(bool c)
    {
        animator.SetBool("_shot", c);
    }
    public void Damager(bool c)
    {
        animator.SetBool("_damage", c);
    }
    public void Jump2(bool c)
    {
        animator.SetBool("_jump2", c);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
