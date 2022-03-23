using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBossAreas : MonoBehaviour
{
    private InputManager _player;

    [SerializeField]
    private GameObject _whiteFade;
    private Animator _animator;

    [SerializeField]
    private GameObject _rocks;

    [SerializeField]
    private AudioClip _clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<InputManager>();

        if (_player != null)
        {
            PlayerAccess.Instance.Input.enabled = false;
            PlayerAccess.Instance.Movement.Move(Vector2.zero);
            PlayerAccess.Instance.Animation.Run(false);
            PlayerAccess.Instance.Animation.OffShot();

            ShakingCamera.Instance.ShakeCamera(5, 1);
            SoundManager.Instance.PlayEffectSound(_clip);

            _animator.Play("FadeIn", 0, 0f);

            _rocks.SetActive(true);

            SoundManager.Instance.Boss();


            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = _whiteFade.GetComponent<Animator>();
        _animator.Play("Rest", 0, 0f);
        _rocks.SetActive(false);

    }

}
