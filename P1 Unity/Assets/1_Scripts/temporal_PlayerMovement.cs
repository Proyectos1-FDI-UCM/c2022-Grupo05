using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporal_PlayerMovement : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _movementSpeed = 1.0f;

 
    #endregion

    #region references
    private Transform _myTransform;
    #endregion

    #region properties
    private Vector3 _movementDirection;
    #endregion



    // m�todo para establecer la direcci�n deseada para el movimiento.
    #region methods
    public void SetMovementDirection(Vector3 newMovementDirection)  // como p�rametro de entrada se crea una variable "newMovementDirection"
                                                                    // para coger datos mandados por el Script "ShipInputComponent" 
    {
        _movementDirection = newMovementDirection;
    }
    #endregion

   

    void Start()
    {
        _myTransform = transform;

    }
    void Update()
    {
        _myTransform.Translate(_movementSpeed * _movementDirection * Time.deltaTime);

    }
}
