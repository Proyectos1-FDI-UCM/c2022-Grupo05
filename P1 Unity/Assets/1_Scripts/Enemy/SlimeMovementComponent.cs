using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovementComponent : MonoBehaviour
{

    #region parameters
    [SerializeField] private Vector3[] _path;
    [SerializeField] private float _speed = 2; 

    #endregion

    #region properties
    private int _pointer;
    #endregion

    #region references
    private Transform _myTransform;
    #endregion

    #region methods
    private void UpdateDirection()
    {
        _myTransform.position = _path[_pointer];
        _myTransform.Rotate(0, 0, -90);
        _pointer++;
        if (_pointer >= _path.Length) 
        {
            _pointer = 0;
        }
    }

    private bool PositionReached() 
    {
        switch (_myTransform.eulerAngles.z) 
        {
            case 0: return (_myTransform.position.x > _path[_pointer].x);
            case 270: return (_myTransform.position.y < _path[_pointer].y);
            case 180: return (_myTransform.position.x < _path[_pointer].x);
            case 90: return (_myTransform.position.y > _path[_pointer].y);
            default: return false;
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _pointer = 0;
        _myTransform.position = _path[_pointer];
        _myTransform.rotation = Quaternion.identity;
        _pointer++;
    }

    // Update is called once per frame
    void Update()
    {
        _myTransform.Translate(Vector2.right * _speed * Time.deltaTime);
        if (PositionReached()) UpdateDirection();
    }
}
