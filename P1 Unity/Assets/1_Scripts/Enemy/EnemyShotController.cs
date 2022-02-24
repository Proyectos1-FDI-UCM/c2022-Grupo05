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
    #endregion

    #region references
    private Transform _shootPoint;
    [SerializeField] private Transform _playerPos;
    [SerializeField] private GameObject _shotPrefab;
    #endregion

    public void Shoot()
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
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootCooldown > 0) _shootCooldown -= Time.deltaTime;
        else Shoot();
    }
}
