using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeComponent : MonoBehaviour
{
    #region references 
    private Animator _animator;
    private SpriteRenderer _playerRenderer;

    [SerializeField] private AudioClip _damageClip;
    [SerializeField] private AudioClip _gameOverClip;

    #endregion


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

    private bool _parpadea = true;
    private float _frequencyFlash = 0;
    #endregion


    #region properties
    [SerializeField]
    private int _currentLife;          // Vida restante

    [SerializeField]
    private int _currentEnergy;        // Energía restante

    private int _hitDamage = 1;

    public bool ActivateGrace {
        set => _activeGracePeriod = value;
    }
    #endregion


    #region methods
    // Método que daña al jugador
    public bool Damage()
    {
        bool returning = !_activeGracePeriod;
        // Si el periodo de gracia no está activo, se activa y baja la vida del jugador
        if (!_activeGracePeriod)
        {
            SoundManager.Instance.PlayEffectSound(_damageClip);

            _animator.SetBool("_damage", true);
            _currentLife -= _hitDamage;
            Debug.Log("Damaged");
            _activeGracePeriod = true;

            // Activa el contador del periodo de gracia
            _timer = _gracePeriod;

            if (_currentLife <= 0)
            {
                SoundManager.Instance.PlayEffectSound(_gameOverClip);

                _currentLife = 0;
                GameManager.Instance.OnPlayerDeath(gameObject);
            }

            HUDController.Instance.UpdatePlayerHP(_currentLife);
        }
        return returning;
    }

    // Método que cura al jugador
    public void Heal(int amount)
    {
        _currentLife += amount;

        if (_currentLife > _maxLife)
        {
            _currentLife = _maxLife;
        }

        HUDController.Instance.UpdatePlayerHP(_currentLife);

        Debug.Log("Healed");
    }


    public void GetEnergy(int amount)
    {
        _currentEnergy += amount;

        if (_currentEnergy > _maxEnergy)
        {
            _currentEnergy = _maxEnergy;
        }

        HUDController.Instance.UpdateEnergy(_currentEnergy);
    }

    public bool UseEnergy() {
        if(_currentEnergy > 0) {
            _currentEnergy--;
            HUDController.Instance.UpdateEnergy(_currentEnergy);
            return true;
        }
        return false;
    }

    public void SetPlayer(int life, int energy)
    {
        _currentEnergy = energy;
        _currentLife = life;

        if (_currentLife > _maxLife) _currentLife = _maxLife;


        if (_currentEnergy > _maxEnergy) _currentEnergy = _maxEnergy;

        HUDController.Instance.UpdateEnergy(_currentEnergy);
        HUDController.Instance.UpdatePlayerHP(_currentLife); 
    }

    public void SavePlayer() 
    {
        PlayerPrefs.SetInt("Vida", _currentLife);
        PlayerPrefs.SetInt("Carga", _currentEnergy);
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la vida y energía restante con sus valores m�ximos
        _currentLife = _maxLife;
        HUDController.Instance.UpdatePlayerHP(_currentLife);
        _currentEnergy = 0;
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
