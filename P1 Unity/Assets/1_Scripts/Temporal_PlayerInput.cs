using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporal_PlayerInput : MonoBehaviour
{



    /// <summary>
    /// Stores vertical input
    /// </summary>
    private float _horizontal;


    private temporal_PlayerMovement _myPlayerMovement;

    

    void Start()
    {
        _myPlayerMovement = GetComponent<temporal_PlayerMovement>();


    }



    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");


        _myPlayerMovement.SetMovementDirection(new Vector3(_horizontal,0,0));


    }
}