using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupComponent : MonoBehaviour
{
    #region references
    private PlayerLifeComponent _playerLife;
    private Transform _myTransform;


    [SerializeField] private AudioClip _energyClip;
    [SerializeField] private AudioClip _healClip;
    [SerializeField] private AudioClip _rechargeClip;
    [SerializeField] private AudioClip _stoneClip;

    #endregion




    #region parameters
    private float _timer = 0f;      // Tiempo que dura el movimiento

    [SerializeField]                    
    private float _speed = 1f;      // Velocidad del movimiento
    [SerializeField]
    private float _time = 1f;
    #endregion


    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
         _playerLife = collision.GetComponent<PlayerLifeComponent>();

        if (_playerLife != null)
        {
            if (_myTransform.tag == "Energy")
            {
                GameManager.Instance.AddEnergy(1);
                SoundManager.Instance.PlayEffectSound(_energyClip);

                Debug.Log("Energy picked");
            }

            else if (_myTransform.tag == "Health")
            {
                _playerLife.Heal(1);
                SoundManager.Instance.PlayEffectSound(_healClip);

                Debug.Log("Health picked");
            }

            else if (_myTransform.tag == "Recharge")
            {
                _playerLife.GetEnergy(1);
                SoundManager.Instance.PlayEffectSound(_rechargeClip);

                Debug.Log("Recharge picked");
            }
            else
            {
                SoundManager.Instance.PlayEffectSound(_stoneClip);
            }
            Destroy(gameObject);
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

            // Cambia la dirección del movimiento y resetea el temporizador
            if (_timer > _time)
            {
                _speed = -_speed;
                _timer = 0f;
            }
        }
}
