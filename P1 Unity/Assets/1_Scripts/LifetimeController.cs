using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _lifetime = 2.0f;
    #endregion

    // Update is called once per frame
    void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
