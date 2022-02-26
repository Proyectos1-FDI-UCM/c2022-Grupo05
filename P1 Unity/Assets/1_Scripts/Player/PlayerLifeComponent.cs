using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{

    #region parameters
    [SerializeField]
    private int _maxLife = 5;           // Vida inicial

    [SerializeField]
    private int _maxEnergy = 3;         //Energía del traje

    [SerializeField]
    private bool _activeGracePeriod = false;  // Indica si se ha activado el periodo de gracia 

    [SerializeField]
    private float _gracePeriod = 3f;          // Tiempo que dura el periodo de gracia
    private float _timer = 0f;                // Tiempo que lleva activo el periodo de gracia
    #endregion


    #region properties
    [SerializeField]
    private int _currentLife;          // Vida restante

    [SerializeField]
    private int _currentEnergy;       // Energía restante
    #endregion


    #region methods
    // Método que daña al jugador
    public void Damage()
    {
        // Si el periodo de gracia no está activo, se activa y baja la vida del jugador
        if (_timer <= _gracePeriod && !_activeGracePeriod)
        {
            _currentLife--;

            Debug.Log("Damaged");

            //GameManager.Instance.OnPlayerDamage(_currentLife);

            // ** LA COMPROBACIÓN DE SI EL JUGADOR HA MUERTO LA REALIZA EL GAMEMANAGER **

            // Activa el contador del periodo de gracia
            _timer = _gracePeriod;
        }
    }

    // Método que cura al jugador
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


    // Método que daña al jugador
    public void UseEnergy()
    {
        // Si el periodo de gracia no está activo, se activa y baja la vida del jugador
        if (_currentEnergy > 0)
        {
            _currentEnergy -= 1;

            Debug.Log("Energy used");

            //GameManager.Instance.OnPlayerDamage(_currentEnergy);
        }
    }

    // Método que cura al jugador
    public void GetEnergy()
    {
        _currentEnergy += 1;

        if (_currentEnergy > _maxEnergy)
        {
            _currentEnergy = _maxEnergy;
        }

        Debug.Log("Energy recharged");

        //GameManager.Instance.OnPlayerDamage(_currentEnergy);
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la vida y energía restante con sus valores máximos
        _currentLife = _maxLife;

        _currentEnergy = _maxEnergy;

        // Inicializa la vida inicial en el GameManager
        //GameManager.Instance.OnPlayerDamage(_maxLife);
    }


    // Update is called once per frame
    void Update()
    {
        _activeGracePeriod = false;

        // Desactiva el daño que recibe el jugador y actualiza el temporizador
        if (_timer > 0)
        {
            _activeGracePeriod = true;

            _timer -= Time.deltaTime;
        
          //  Debug.Log("Invincible");
        }
    }
   
}
