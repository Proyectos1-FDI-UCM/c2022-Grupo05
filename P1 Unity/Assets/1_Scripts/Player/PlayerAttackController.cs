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
    #endregion

    #region methods
    public void Shoot() 
    {
        if (_shootCooldown <= 0)
        {
            GameObject shot = Instantiate(_shotPrefab, _shootPoint.position, _shootPoint.rotation);
            shot.GetComponent<ShotMovementController>().SetDirection(_shootPoint.rotation.y == 180 ? Vector2.left : Vector2.right);
            _shootCooldown = _shootTime;
        }
    }
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
