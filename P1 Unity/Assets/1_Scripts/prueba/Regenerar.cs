using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerar : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;
    private Vector3 _pos;
    private PickupComponent _obj;
    private void Generar()
    {
        Instantiate(_object, _pos, Quaternion.identity);
    }
    public void Generar2()
    {
        Invoke("Generar", 0.3f);
    }
    private void Start()
    {
        _pos = transform.position;
    }
}
