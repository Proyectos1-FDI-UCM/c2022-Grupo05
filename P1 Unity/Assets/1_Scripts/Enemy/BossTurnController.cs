using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurnController : MonoBehaviour
{
    [SerializeField]
    private float _cont=0;
    [SerializeField]
    private bool _giro = false;
    public void Giro()
    {       
       float rnd =GameManager.Instance.RNG(0, 4);
       Vector3 r = Vector3.one; //new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z);
        if (rnd > 2)
        {
            r.x=-1;
            if (rnd <3)
            {
                r.y=-1;
            }
        }
        else
        {
            r.y=-1;
            if (rnd<1)
            {
                r.x=-1;
            }
        }
        transform.localScale = Vector3.Scale(transform.localScale, r);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cont += Time.deltaTime;
        if (_giro)
        {
            Giro();
            _giro = false;
        }
    }
}
