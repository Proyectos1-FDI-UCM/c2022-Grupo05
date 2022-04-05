using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    #region references
    [SerializeField] private EventSystem _eventSys;
    #endregion

    #region methods
    public void ReturnMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
