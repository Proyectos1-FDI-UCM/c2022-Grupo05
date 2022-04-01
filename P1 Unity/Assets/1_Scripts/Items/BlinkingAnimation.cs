using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingAnimation : MonoBehaviour
{
    #region references 
    [SerializeField]
    private SpriteRenderer _object;
    #endregion


    #region parameters
    [SerializeField]
    private float _activetime = 5.0f;
    private float _time;
    [SerializeField]
    private float _frecuencia = 0.2f;
    private bool _activado = false;
    private bool _set = false;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _object.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _activetime)
        {
            _activado = true;
            _time = 0;
        }
        if (_activado)
        {
            if (_time > _frecuencia)
            {
                _time = 0;
                _object.enabled = _set;
                _set = !_set;
            }
        }
        
    }
}
