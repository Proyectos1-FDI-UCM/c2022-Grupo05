using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootSpeed = 10.0f;
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
        _myTransform.Translate((_myTransform.rotation.y == 0 ? Vector2.left : Vector2.right) * _shootSpeed * Time.deltaTime);
    }
}
