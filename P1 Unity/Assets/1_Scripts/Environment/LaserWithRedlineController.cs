using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))] //añade por defecto un componente de tipo boxCollider

public class LaserWithRedlineController : MonoBehaviour
{

    [SerializeField]
    private float _activeTime = 5f;
    [SerializeField]
    private float _desactiveTime = 3f;
    [SerializeField]
    private float _redLineTime = 2f;

    private float _elapsedTime = 0.0f;

    private bool _active = true;

    [SerializeField] private GameObject _electricity;
    [SerializeField] private GameObject _lineaRoja;

    private ProximitySound _proximitySound;




    private void OnEnable()
    {
        _elapsedTime = 0;
        _electricity.GetComponent<Renderer>().enabled = false;
        _electricity.GetComponent<Collider2D>().enabled = false;
        _active = false;
        _lineaRoja.SetActive(false);
    }
    void Start()
    {
        _proximitySound = GetComponent<ProximitySound>();
        _active = true;
        _lineaRoja.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        _elapsedTime += 1 * Time.deltaTime; //en seg

        //activado
        if (_active && _elapsedTime > _activeTime)
        {
            _elapsedTime = 0;
            _electricity.GetComponent<Renderer>().enabled = false;
            _electricity.GetComponent<Collider2D>().enabled = false;
            _active = false;

        }
        else if (!_active && _elapsedTime > _desactiveTime)
        {
            _proximitySound.PlaySound();
            _lineaRoja.SetActive(false);
            _elapsedTime = 0;
            _electricity.GetComponent<Renderer>().enabled = true;
            _electricity.GetComponent<Collider2D>().enabled = true;
            _active = true;


        }
        else if (!_active && _elapsedTime + _redLineTime > _desactiveTime)
        {
            _lineaRoja.SetActive(true);
        }
    }
}
