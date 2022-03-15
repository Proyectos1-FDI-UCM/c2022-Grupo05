using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevesItemControlerer : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _atacktime = 10;         //para cada ataque especial
    private float _atackcont = 0;
    private bool _atack = false;
    [SerializeField]
    private float _timeofshot = 12;  // tiempo para cada disparar
    private float _cont =0;       //contador para disparos normal
    private bool _shot = false;
    #endregion

    #region properties
    private bool _activa = false;
    [SerializeField]
    private int _bosslife;
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
    #endregion

    #region methods
    private void Shot()
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
        int rnd = Random.RandomRange(0, 10);
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
            Shot();
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
        if (_atack)
        {
            BossAtack();
            _atack = false;
        }
        else
        {
            _atackcont += Time.deltaTime;
            if (_atackcont > _atacktime)
            {
                _atack = true;
                _atackcont = 0;
            }
        }
    }
}
