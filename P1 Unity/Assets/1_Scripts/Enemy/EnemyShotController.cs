using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _shootTime = 0.5f;
    #endregion

    #region properties
    private float _shootCooldown;
    private bool _playerinRange;
    #endregion

    #region references
    private Transform _shootPoint;
    private Transform _playerPos;
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private EnemyDetection _detection;
    #endregion
    private void Detectplayer()
    {
        _playerinRange = true;
    }
    private void F()
    {
        _playerinRange = false;
    }
    private void Shoot()
    {
        
        {
            GameObject shot = Instantiate(_shotPrefab, _shootPoint.position, _shootPoint.rotation);
            shot.GetComponent<ShotMovementController>().SetDirection(Vector3.Normalize(_playerPos.position - transform.position));
            _shootCooldown = _shootTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _shootPoint = transform;
        _shootCooldown = 0;
        _detection.accionEntrar += Detectplayer;
        _detection.accionSalir += F;
        _playerPos = PlayerAccess.Instance.Transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootCooldown > 0) _shootCooldown -= Time.deltaTime;
        else if (_playerinRange) Shoot();
    }
}
