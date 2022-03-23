using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootSpeed = 15.0f;
    private Vector2 dir = Vector2.right;
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
        if(direction.x >= 0 && direction.y >= 0) _myTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Asin(_direction.y), Vector3.forward);
        else if(direction.x < 0 && direction.y >= 0) _myTransform.rotation = Quaternion.AngleAxis(180 - Mathf.Rad2Deg * Mathf.Asin(_direction.y), Vector3.forward);
        else if(direction.x < 0 && direction.y < 0) _myTransform.rotation = Quaternion.AngleAxis(180 - Mathf.Rad2Deg * Mathf.Asin(_direction.y), Vector3.forward);
        else _myTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Asin(_direction.y), Vector3.forward);
    }
    public void SetDirectionPlayerShot(Vector2 direction)
    {
        dir = direction;
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
        _myTransform.Translate(dir* _shootSpeed * Time.deltaTime);
    }
}
