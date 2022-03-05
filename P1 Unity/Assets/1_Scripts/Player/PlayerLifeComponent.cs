using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{
    #region references 
    private Animator _animator;
    private SpriteRenderer _playerRenderer;
    #endregion

    #region parameters
    [SerializeField]
    private int _maxLife = 5;           // Vida inicial

    [SerializeField]
    private int _maxEnergy = 3;         // Energ�a inicial

    [SerializeField]
    private bool _activeGracePeriod = false;  // Indica si se ha activado el periodo de gracia 

    [SerializeField]
    private float _gracePeriod = 3f;          // Tiempo que dura el periodo de gracia
    private float _timer = 0f;                // Tiempo que lleva activo el periodo de gracia

    private bool _parpadea = true;
    private float _frequencyFlash = 0;
    #endregion


    #region properties
    [SerializeField]
    private int _currentLife;          // Vida restante

    [SerializeField]
    private int _currentEnergy;        // Energ�a restante
    #endregion


    #region methods
    // M�todo que da�a al jugador
    public void Damage()
    {
        // Si el periodo de gracia no est� activo, se activa y baja la vida del jugador
        if (!_activeGracePeriod)
        {
            _animator.SetBool("_damage", true);
            _currentLife--;
            Debug.Log("Damaged");
            _activeGracePeriod = true;
            // ** LA COMPROBACI�N DE SI EL JUGADOR HA MUERTO LA REALIZA EL GAMEMANAGER **

            // Activa el contador del periodo de gracia
            _timer = _gracePeriod;

            if (_currentLife <= 0)
            {
                _currentLife = 0;
                GameManager.Instance.OnPlayerDeath(gameObject);
            }

            HUDController.Instance.UpdateHP(_currentLife);
        }
    }

    // M�todo que cura al jugador
    public void Heal(int amount)
    {
        _currentLife += amount;

        if (_currentLife > _maxLife)
        {
            _currentLife = _maxLife;
        }

        HUDController.Instance.UpdateHP(_currentLife);

        Debug.Log("Healed");
    }


    // M�todo que da�a al jugador
    public void UseEnergy()
    {
        // Si el periodo de gracia no est� activo, se activa y baja la vida del jugador
        if (_currentEnergy > 0)
        {

            _currentEnergy -= 1;

            HUDController.Instance.UpdateEnergy(_currentEnergy);

            Debug.Log("Energy used");
        }
    }

    // M�todo que cura al jugador
    public void GetEnergy()
    {
        _currentEnergy += 1;

        if (_currentEnergy > _maxEnergy)
        {
            _currentEnergy = _maxEnergy;
        }

        HUDController.Instance.UpdateEnergy(_currentEnergy);

        Debug.Log("Energy recharged");
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la vida y energ�a restante con sus valores m�ximos
        _currentLife = _maxLife;
        HUDController.Instance.UpdateHP(_currentLife);
        _currentEnergy = _maxEnergy;
        HUDController.Instance.UpdateEnergy(_currentEnergy);
        _animator = GetComponent<Animator>();
        _playerRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        // Desactiva el da�o que recibe el jugador y actualiza el temporizador
        if (_activeGracePeriod)
        {
            _timer -= Time.deltaTime;
            _frequencyFlash += Time.deltaTime;
            if (_frequencyFlash > 0.2)
            {
                _parpadea = !_parpadea;
                _playerRenderer.enabled = _parpadea;
                _frequencyFlash = 0;
            }
            if (_timer < 0)
            {
                _activeGracePeriod = false;
                _playerRenderer.enabled = true;
            }
           
        }
    }

}
