using UnityEngine;

public class BossLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _maxLife = 40;
    [SerializeField] private int _breakPointLife = 20; //donde pasa a la segunda fase
    [SerializeField] private bool _secondPhase;
    #endregion

    #region properties
    private int _currentLife;
    #endregion

    #region references
    [SerializeField] private GameObject _DieFX;
    [SerializeField] private GameObject _fullBody;
    [SerializeField] private GameObject _weakPoint;
    [SerializeField] private GameObject _pared;
    [SerializeField] private Transform[] _point; //lugares que generar vida o enegia para la mejora
    [SerializeField] private GameObject _Cure;
    [SerializeField] private GameObject _Energy;
    //private GameObject _bossObject;

    [SerializeField] private AudioClip _damageClip;
    [SerializeField] private AudioClip _damageClip1;
    [SerializeField] private AudioClip _damageClip2;
    [SerializeField] private AudioClip _damageClip3;

    #endregion

    #region methods
    public void Damage(bool isShotUpgraded)
    {
        InstanceObject();

        float _rnd = GameManager.Instance.RNG(0, 3);
        if (_rnd > 2)
        {
            SoundManager.Instance.PlayEffectSound(_damageClip1);
        }
        else if (_rnd > 1)
        {
            SoundManager.Instance.PlayEffectSound(_damageClip2);
        }
        else
        {
            SoundManager.Instance.PlayEffectSound(_damageClip3);
        }
        SoundManager.Instance.PlayEffectSound(_damageClip);

        _currentLife--;
        if (isShotUpgraded) _currentLife--;
        if (!_secondPhase && _currentLife <= _breakPointLife)
        {
            _currentLife = _breakPointLife;

            _weakPoint.GetComponent<Collider2D>().enabled = true;
            _fullBody.GetComponent<Collider2D>().enabled = false;
            _weakPoint.SetActive(true);
            GetComponentInParent<BossTransitionAnimation>().enabled = true;

        } //cambio fase
        if (_currentLife <= 0)
        {
            HUDController.Instance.ShowBossBar(false);
           // _bossObject.SetActive(false);
            Instantiate(_DieFX, transform.position, Quaternion.identity);
            _pared.SetActive(false);
            GameObject.Find("BossSecondPhase").SetActive(false);
        }
        HUDController.Instance.UpdateBossHP(_currentLife);
    }
    private void InstanceObject()   //genera cura o energia
    {
        float i = GameManager.Instance.RNG(0, 10);
        if (i<1f)
        {
            Instantiate(_Cure, _point[(int)GameManager.Instance.RNG(0, _point.Length)].position,Quaternion.identity);
        }
        else if (i <2)
        {
            Instantiate(_Energy, _point[(int)GameManager.Instance.RNG(0, _point.Length)].position,Quaternion.identity);
        }
 
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentLife = _maxLife;

        if (_secondPhase)
        {
            _weakPoint.SetActive(true);
            _weakPoint.GetComponent<Collider2D>().enabled = true;
            _fullBody.GetComponent<Collider2D>().enabled = false;
            // _bossObject.SetActive(true);
        }
        else
        {
            _weakPoint.SetActive(false);
            _weakPoint.GetComponent<Collider2D>().enabled = false;
            _fullBody.GetComponent<Collider2D>().enabled = true;
            //_bossObject.SetActive(false);
        }
    }
}
