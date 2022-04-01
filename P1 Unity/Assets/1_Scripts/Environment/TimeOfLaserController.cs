using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfLaserController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _laser1;
    [SerializeField]
    private GameObject[] _laser2;
    private bool _enableLaser1 = true;
    private bool _enableLaser2 = false;

    [SerializeField] private float _time = 2.5f;

    private float cont = 0;

    AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;



    private void LaserEnable(GameObject[] laser,bool enable)
    {
        for(int i = 0; i < laser.Length; i++)
        {

            laser[i].SetActive(enable);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        LaserEnable(_laser1, _enableLaser1);
        LaserEnable(_laser2, _enableLaser2);
    }

    // Update is called once per frame
    void Update()
    {
        cont += Time.deltaTime;
        if (cont > _time)
        {
            _audioSource.PlayOneShot(_clip);

            _enableLaser1 = !_enableLaser1;
            _enableLaser2 = !_enableLaser2;
            LaserEnable(_laser1, _enableLaser1);
            LaserEnable(_laser2, _enableLaser2);
            cont = 0;
        }
    }
}
