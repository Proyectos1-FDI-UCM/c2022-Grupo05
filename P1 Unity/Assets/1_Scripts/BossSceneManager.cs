using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
    [SerializeField] GameObject Area1;
    [SerializeField] GameObject Area2;
    [SerializeField] GameObject[] _rock;

    [SerializeField] GameObject _boss1;
    [SerializeField] GameObject _boss2;

    private bool place2 = false;
    // Start is called before the first frame update
    void Awake()
    {

        _rock[0].SetActive(false);
        _rock[2].SetActive(false);
        Area1.SetActive(true);
        Area2.SetActive(true);
        _boss1.SetActive(false);
        _boss2.SetActive(false);
    }
    private void Start()
    {
        transform.parent = PlayerAccess.Instance.transform;
    }
    private void Fase1()
    {
        SoundManager.Instance.Level();

        _rock[0].SetActive(false);
        _rock[2].SetActive(false);
        Area1.SetActive(true);
        Area2.SetActive(true);
        _boss1.SetActive(false);
        _boss2.SetActive(false);
    }
    private void Fase2()
    {
        _rock[0].SetActive(false);
        _rock[1].SetActive(false);
        _rock[2].SetActive(false);
        Area1.SetActive(false);
        Area2.SetActive(true);
        _boss2.SetActive(false);
    }
    private void OnEnable()
    {
        if (place2)
        {
            Fase2();
        }
        else
        {
            Fase1();
        }
    }
    public void SetFase()
    {
        place2 = true;
    }

}
