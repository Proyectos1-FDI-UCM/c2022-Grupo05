using System.Collections;
using System.Collections.Generic;
using UnityEngine; // a

public class BossSecondAttackController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _atacktime = 5;  //para cada ataque especial
    [SerializeField]
    private float _timeofshot = 12; // tiempo para giro
    [SerializeField]
    private float _timeBetweenShots = 0.3f;        // Intervalo entre balas (volea de balas)
    #endregion

    #region properties
    private float _atackcont = 0;   // Contador para intervalo entre ataques
    private float _bosstruncont = 0; //Contador para giro de boss
    private float _rnd;
    #endregion

    #region references
    private Transform _MyTransfrom;
    [SerializeField]
    private GameObject _laserHorizontal;
    [SerializeField]
    private GameObject _laserVertical;
    [SerializeField]
    private GameObject _8balas;
    [SerializeField]
    private GameObject _bala;
    [SerializeField]
    private GameObject _shroomPrefab;
    private Transform _player;

    [SerializeField] private AudioClip _rotateClip;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioClip _mushroomClip;

    #endregion

    #region methods

    private void Rotate() {
        SoundManager.Instance.PlayEffectSound(_rotateClip);

        float rnd = GameManager.Instance.RNG(0, 4);
        Vector3 r = Vector3.one; //new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z);
        if (rnd > 2)
        {
            r.x = -1;
            if (rnd < 3)
            {
                r.y = -1;
            }
        }
        else
        {
            r.y = -1;
            if (rnd < 1)
            {
                r.x = -1;
            }
        }
        _MyTransfrom.localScale = Vector3.Scale(transform.localScale, r);
        //_MyTransfrom.localScale = new Vector3(_MyTransfrom.localScale.x, -_MyTransfrom.localScale.y, _MyTransfrom.localScale.z);
    }
    private void ShootVolley() {
        SoundManager.Instance.PlayEffectSound(_shootClip);

        Shoot();
        Invoke("Shoot", _timeBetweenShots);
        Invoke("Shoot", 2 * _timeBetweenShots);
    }
    private void Shoot() {
        SoundManager.Instance.PlayEffectSound(_shootClip);

        GameObject shot = Instantiate(_bala, _MyTransfrom.position, Quaternion.identity);
        shot.GetComponent<ShotMovementController>().SetDirection(Vector3.Normalize(_player.position - transform.position));
    }
    private void ShotCircle()
    {
        SoundManager.Instance.PlayEffectSound(_shootClip);
        Instantiate(_8balas,_MyTransfrom.position,Quaternion.identity);
    }
    private void ShroomShot()
    {
        SoundManager.Instance.PlayEffectSound(_mushroomClip);

        GameObject shot = Instantiate(_shroomPrefab, _MyTransfrom.position, _MyTransfrom.rotation);
        shot.GetComponent<ShotMovementController>().SetDirection(Vector3.Normalize(_player.position - transform.position));
    }
    private void LaserDoble()
    {
        LaserH();
        LaserV();
    }
    private void LaserH()
    {
        _laserHorizontal.SetActive(true);
    }
    private void LaserV()
    {
        _laserVertical.SetActive(true);
    }
    private void Apagar()
    {
        _laserVertical.SetActive(false);
        _laserHorizontal.SetActive(false);
    }
    private void BossAtack()
    {
       // ShootVolley();
        float rnd = GameManager.Instance.RNG(0, 10);
        if (rnd < 1)
        {
            LaserDoble();
        }
        else if (rnd <2.5)
        {
            LaserV();
        }
        else if (rnd < 4)
        {
            LaserH();
        }
        else if(rnd<5.5)
        {
            ShotCircle();
        }
        else if (rnd < 8)
        {
            ShroomShot();
        }
        else
        {
            ShootVolley();
        }
        Invoke("Apagar", 4f);
    }
    private void OnDisable()
    {
        Apagar();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _MyTransfrom = transform;
        _player = PlayerAccess.Instance.Transform;
        _rnd = GameManager.Instance.RNG(6, 12);
    }

    // Update is called once per frame
    void Update()
    {
        _atackcont += Time.deltaTime;
        if (_atackcont > _atacktime)
        {
            BossAtack();
            _atackcont = 0;
        }
        _bosstruncont += Time.deltaTime;
        if (_bosstruncont > _rnd)
        {
            _bosstruncont = 0;
            _rnd = GameManager.Instance.RNG(4,10);
            Rotate();
        }
        
    }
}
