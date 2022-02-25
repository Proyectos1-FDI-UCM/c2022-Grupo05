using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootSpeed = 15.0f;
    #endregion

    #region properties
    private Vector2 _direction;
    #endregion

    #region references
    private Transform _myTransform;
    #endregion

    #region methods
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _myTransform.Translate(_direction * _shootSpeed * Time.deltaTime);
    }
}
