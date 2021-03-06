using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBossAreas : MonoBehaviour
{
    private InputManager _player;
    private BossInAreaTrigger _bossInArea;

    [SerializeField]
    private GameObject _whiteFade;
    private Animator _animator;

    [SerializeField]
    private GameObject _rocks;

    [SerializeField]
    private AudioClip _avalancheClip;
    [SerializeField]
    private AudioClip _rocksClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<InputManager>();

        if (_player != null)
        {
            PlayerAccess.Instance.Input.enabled = false;
            PlayerAccess.Instance.Movement.Move(Vector2.zero);
            PlayerAccess.Instance.Animation.Run(false);
            PlayerAccess.Instance.Animation.OffShot();

            ShakingCamera.Instance.ShakeCamera(5, 3);
            SoundManager.Instance.PlayEffectSound(_avalancheClip);

            PlayerAccess.Instance.Movement.Move(new Vector2(1.5f,0));

            _animator.SetTrigger("Start");
            StartCoroutine(PlaceRocks());

        }
    }


    IEnumerator PlaceRocks()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAccess.Instance.Movement.Move(Vector2.zero);
        _bossInArea.SpawnBoss();

        yield return new WaitForSeconds(1.55f);
        _rocks.SetActive(true);
        SoundManager.Instance.PlayEffectSound(_rocksClip);
        SoundManager.Instance.Boss();

        StartCoroutine(DeactivateTrigger());
    }

    IEnumerator DeactivateTrigger()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        _bossInArea = GetComponent<BossInAreaTrigger>();
        _animator = _whiteFade.GetComponent<Animator>();
        _rocks.SetActive(false);
    }

}
