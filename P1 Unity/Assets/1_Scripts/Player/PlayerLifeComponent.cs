using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{

    #region parameters
    [SerializeField]
    private int _maxLife = 3;           // Vida inicial

    [SerializeField]
    private int _hitDamage = 1;         // Vida perdida al recibir daño

    [SerializeField]
    private float _gracePeriod = 3f;          // Tiempo que dura el periodo de gracia
    private float _timer = 0f;                // Tiempo que lleva activo el periodo de gracia
    #endregion


    #region properties
    [SerializeField]
    private int _currentLife;          // Vida restante
    #endregion


    #region methods
    public void Damage()
    {
        // Si el periodo de gracia no está activo, se activa y baja la vida del jugador
        if (_timer <= _gracePeriod)
        {
            _currentLife -= _hitDamage;

            Debug.Log("Damaged");

            //GameManager.Instance.OnPlayerDamage(_currentLife);

            // ** LA COMPROBACIÓN DE SI EL JUGADOR HA MUERTO LA REALIZA EL GAMEMANAGER **

            // Activa el contador del periodo de gracia
            _timer = _gracePeriod;
        }
    }


    public void Heal()
    {
        _currentLife += 1;

        if (_currentLife > _maxLife)
        {
            _currentLife = _maxLife;
        }

        Debug.Log("Healed");

        //GameManager.Instance.OnPlayerDamage(_currentLife);
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la vida restante con el valor de la vida máxima
        _currentLife = _maxLife;


        // Inicializa la vida inicial en el GameManager
        //GameManager.Instance.OnPlayerDamage(_maxLife);
    }


    // Update is called once per frame
    void Update()
    {
        _hitDamage = 1;

        // Desactiva el daño que recibe el jugador y actualiza el temporizador
        if (_timer > 0)
        {
            _hitDamage = 0;

            _timer -= Time.deltaTime;
           // Debug.Log("Invincible");
        }
    }
   
}
