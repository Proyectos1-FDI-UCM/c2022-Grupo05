using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupComponent : MonoBehaviour
{
    #region references
    private PlayerLifeComponent _playerLife;

    private Transform _myTransform;
    #endregion


    #region parameters
    private float _timer = 0f;      // Tiempo que dura el movimiento

    [SerializeField]                    
    private float _speed = 1f;      // Velocidad del movimiento
    #endregion


    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerLife = collision.GetComponent<PlayerLifeComponent>();

        if (_playerLife != null)
        {
            Destroy(gameObject);

            if (_myTransform.tag == "Energy")
            {
                // A�adir contador de energ�a en el GameManager
                //GameManager.Instance.AddEnergy(1);

                Debug.Log("Energy picked");
            }

            // A�adir m�s tags para los kits de vida, setas, o sobrecargas de energ�a
        }
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
        _timer += Time.deltaTime;

        // Movimiento del objeto
        _myTransform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Cambia la direcci�n del movimiento y resetea el temporizador
        if(_timer > 0.7f)
        {
            _speed = -_speed;
            _timer = 0f;
        }
    }
}
