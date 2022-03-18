using System.Collections;
using System.Collections.Generic;
using UnityEngine; // a

public class BossSecondAttackController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _atacktime = 10;  //para cada ataque especial
    [SerializeField]
    private float _timeofshot = 12; // tiempo para cada disparar
    [SerializeField]
    private float _timeBetweenShots = 0;        // Intervalo entre balas (volea de balas)
    #endregion

    #region properties
    private int _bosslife;          // Guarda la vida que tiene el jefe
    private float _atackcont = 0;   // Contador para intervalo entre ataques
    private bool _volley = false;   // Si está disparando una volea
    #endregion

    #region references
    private Transform _MyTransfrom;
    private BossLifeComponent _life;
    [SerializeField]
    private GameObject _laserHorizontal;
    [SerializeField]
    private GameObject _laserVertical;
    [SerializeField]
    private GameObject _8balas;
    [SerializeField]
    private GameObject _bala;
    [SerializeField]
    private Transform _player;
    #endregion

    #region methods
    private void Rotate() {
        _MyTransfrom.localScale = new Vector3(_MyTransfrom.localScale.x, -_MyTransfrom.localScale.y, _MyTransfrom.localScale.z);
    }
    private void ShootVolley() {
        Shoot();
        Invoke("Shoot", _timeBetweenShots);
        Invoke("Shoot", 2 * _timeBetweenShots);
    }
    private void Shoot() {
        GameObject shot = Instantiate(_bala, _MyTransfrom.position, Quaternion.identity);
        shot.GetComponent<ShotMovementController>().SetDirection(Vector3.Normalize(_player.position - transform.position));
    }
    private void ShotCircle()
    {
        Instantiate(_8balas,_MyTransfrom.position,Quaternion.identity);
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
        ShootVolley();
        int rnd = Random.Range(0, 10);
        if (rnd < 1)
        {
            LaserDoble();
        }
        else if (rnd < 4)
        {
            LaserV();
        }
        else if (rnd < 7)
        {
            LaserH();
        }
        else
        {
            ShotCircle();
        }
        Invoke("Apagar", 2f);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _MyTransfrom = transform;
        _life = GetComponent<BossLifeComponent>();
        _bosslife = _life.ActualLife();
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
    }
}
