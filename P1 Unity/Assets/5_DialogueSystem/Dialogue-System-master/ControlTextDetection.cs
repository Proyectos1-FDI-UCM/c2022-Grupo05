using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class ControlTextDetection : MonoBehaviour
{
    [SerializeField]
    private ControlText _controlText; 
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovementManager>() != null) 
        {
            Debug.Log("Control");
            _controlText.ShowControl();
        }
        Destroy(gameObject);
    }
}
