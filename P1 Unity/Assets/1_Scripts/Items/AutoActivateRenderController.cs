using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] //añade por defecto un componente de tipo boxCollider
public class AutoActivateRenderController : MonoBehaviour
{
    [SerializeField]
    private float _activeTime = 5f;
    [SerializeField]
    private float _desactiveTime = 3f;

    private float _elapsedTime=0.0f;

    private bool _active = true;

    void Start()
    {
        _active = true;
    }


    // Update is called once per frame
    void Update()
    {
        _elapsedTime += 1 * Time.deltaTime; //en seg
       
        //activado
        if (_active && _elapsedTime>_activeTime)
        {
            _elapsedTime = 0;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            _active = false;

        }

        else if (!_active && _elapsedTime > _desactiveTime)
        {
            _elapsedTime = 0;
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            _active = true;
        }
    }
}
