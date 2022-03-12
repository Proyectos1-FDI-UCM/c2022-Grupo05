using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotMovement : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootSpeed = 15.0f;
    #endregion

    #region properties
    [SerializeField]
    private Vector2 _direction;
    #endregion

    #region references
    private Transform _myTransform;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _myTransform.Translate(_direction.normalized * _shootSpeed * Time.deltaTime);
    }
}
