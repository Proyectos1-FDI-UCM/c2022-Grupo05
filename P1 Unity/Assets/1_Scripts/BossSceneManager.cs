using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject Area1;
    [SerializeField] private GameObject Area2;
    [SerializeField] private GameObject[] _rock;

    [SerializeField] private GameObject _boss1;
    [SerializeField] private GameObject _boss2;
    [SerializeField] private Vector2 _p1;
    [SerializeField] private HUDController _hudController;
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
        _boss1.transform.position = _p1;
    }
    private void Fase2()
    {
        _rock[0].SetActive(false);
        _rock[1].SetActive(false);
        _rock[2].SetActive(false);
        Area1.SetActive(false);
        Area2.SetActive(true);
        _boss2.SetActive(false);
        _boss1.transform.localScale =new Vector3(1.2224f, 1.2224f, 1.2224f);
    }
    private void OnEnable()
    {
        _hudController.ShowBossBar(false);
        if (place2)
        {
            HUDController.Instance.UpdateBossHP(20);
            Fase2();
        }
        else
        {
            HUDController.Instance.UpdateBossHP(40);
            Fase1();
        }
    }
    public void SetFase()
    {
        place2 = true;
    }

}
