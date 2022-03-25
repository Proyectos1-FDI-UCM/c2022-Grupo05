using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootTime = 0.5f;
    #endregion

    #region properties
    private float _shootCooldown;
    #endregion

    #region references
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private GameObject _upgradedShotPrefab;

    [SerializeField] private AudioClip _normalShotClip;
    [SerializeField] private AudioClip _upgShotClip;

    #endregion

    #region methods
    public void Shoot(bool ampPower) 
    {
        if (_shootCooldown <= 0)
        {
            GameObject shot;
            if (ampPower)
            {
                shot = Instantiate(_upgradedShotPrefab, _shootPoint.position, _shootPoint.rotation);
                SoundManager.Instance.PlayEffectSound(_upgShotClip);

            }
            else
            {
                shot = Instantiate(_shotPrefab, _shootPoint.position, _shootPoint.rotation);
                SoundManager.Instance.PlayEffectSound(_normalShotClip);
            }

            shot.GetComponent<ShotMovementController>().SetDirectionPlayerShot(_shootPoint.rotation.y == 180 ? Vector2.left : Vector2.right);
            _shootCooldown = _shootTime;

        }
    }

    public bool ShootReady => _shootCooldown <= 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
       _shootCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_shootCooldown > 0) _shootCooldown -= Time.deltaTime;
    }
}
