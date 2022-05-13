using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootSpeed = 15.0f;
    #endregion

    #region references
    private Transform _myTransform;
    private Rigidbody2D _rigidbody;
    #endregion

    #region methods
    public void SetDirection(Vector2 direction)
    {
        if(direction.x >= 0 && direction.y >= 0) _myTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Asin(direction.y), Vector3.forward);
        else if(direction.x < 0 && direction.y >= 0) _myTransform.rotation = Quaternion.AngleAxis(180 - Mathf.Rad2Deg * Mathf.Asin(direction.y), Vector3.forward);
        else if(direction.x < 0 && direction.y < 0) _myTransform.rotation = Quaternion.AngleAxis(180 - Mathf.Rad2Deg * Mathf.Asin(direction.y), Vector3.forward);
        else _myTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Asin(direction.y), Vector3.forward);
        _rigidbody.velocity = direction * _shootSpeed;
    }
    public void SetDirectionPlayerShot(Vector2 direction)
    {
        _rigidbody.velocity = direction * _shootSpeed;
    }
    #endregion
    private void Awake()
    {
        _myTransform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
