using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlText : MonoBehaviour
{
    #region reference
    [SerializeField]
    private Animator animator;
    #endregion

    #region properties
    [SerializeField]
    private float _showingTime = 5f;
    private float _elapsedTime = 0.0f;
    #endregion

    public void ShowControl()
    {
        Debug.Log("Open");
        animator.SetBool("Control", true);
        _elapsedTime = 0;
        enabled = true;
    }
    private void EndControl()
    {
        animator.SetBool("Control", false);
        enabled = false;
        Destroy(gameObject, 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        _elapsedTime = 0.0f;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += 1 * Time.deltaTime; //en seg
        if (_elapsedTime > _showingTime)
        {
            _elapsedTime = 0;

            EndControl();
        }
    }
}
