using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{

    #region parameters
    [SerializeField]
    private int _maxLife = 5;           // Vida inicial

    [SerializeField]
    private int _maxEnergy = 3;         // Energía inicial

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
    private int _currentEnergy;        // Energía restante
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

            if (_currentLife <= 0)
            {
                _currentLife = 0;
                GameManager.Instance.OnPlayerDeath();
            }

            HUDController.Instance.UpdateHP(_currentLife);
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

        HUDController.Instance.UpdateHP(_currentLife);

        //GameManager.Instance.OnPlayerDamage(_currentLife);
    }

    public void GetEnergy() {
        if(_currentEnergy < _maxEnergy)_currentEnergy += 1;

        HUDController.Instance.UpdateEnergy(_currentEnergy);
    }

    public bool UseEnergy() {
        if(_currentEnergy > 0) {
            _currentEnergy--;
            HUDController.Instance.UpdateEnergy(_currentEnergy);
            return true;
        } else return false;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la vida y energía restante con sus valores máximos
        _currentLife = _maxLife;
        HUDController.Instance.UpdateHP(_currentLife);
        _currentEnergy = _maxEnergy;
        HUDController.Instance.UpdateEnergy(_currentEnergy);
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
