using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaRegenerar : MonoBehaviour
{
    private Regenerar _obj;
    [SerializeField]
    int tipo = 0;
    string t = "heart";
    // Start is called before the first frame update
    void Start()
    {
        if (tipo != 0)
        {
            t = "helmet";
        }
        transform.parent = GameObject.Find(t).transform;
        _obj = GetComponentInParent<Regenerar>();
    }
    private void OnDestroy()
    {
        _obj.Generar2();
    }

}
