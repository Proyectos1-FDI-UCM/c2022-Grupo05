using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransitionAnimation : MonoBehaviour
{
    [SerializeField] Vector2 posiciónDeseada;
    [SerializeField] Vector2 velocidad = new Vector2(1, 1);
    [SerializeField] GameObject pared; 

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Transform paredTransform;

    private uint state = 0;
    private float cont = 0;
    float alpha = 0;
    Vector2 origin;

    [SerializeField]
    private AudioClip _rocksClip;
    private int _rocksClipPlayed = 0;

    private void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.simulated = false;
        paredTransform = pared.transform;
        PlayerAccess.Instance.Input.enabled = false;
        PlayerAccess.Instance.Movement.Move(Vector2.zero);
        PlayerAccess.Instance.Animation.Run(false);
        PlayerAccess.Instance.Animation.OffShot();
        GetComponent<BossFirstPhaseController>().enabled = false;
    }

    private void Update() {
        switch(state) {
            case 0:
                cont += Time.deltaTime;
                if(cont >= 0.75f) {
                    state++;
                    _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                    _transform.rotation = Quaternion.Euler(Vector3.zero);
                    origin = new Vector2(_transform.position.x - Mathf.Abs(posiciónDeseada.y - _transform.position.y), _transform.position.y);
                }
                break;
            case 1:
                alpha = Mathf.Lerp(alpha, Mathf.PI/2, velocidad.y * Time.deltaTime);
                Vector2 c = new Vector2(origin.x + Mathf.Abs(posiciónDeseada.y - origin.y) * Mathf.Cos(alpha), origin.y + (posiciónDeseada.y - origin.y) * Mathf.Sin(alpha));
                _transform.position = c;
                if(Mathf.Abs(_transform.position.y - posiciónDeseada.y) < 0.005) {
                    state++;
                    cont = 0;
                }
                break;
            case 2:
                cont += Time.deltaTime;
                if(cont >= 0.15f) {
                    state++;
                }
                break;
            case 3:
                _transform.Translate(Vector3.Normalize(new Vector3(posiciónDeseada.x - _transform.position.x, 0, 0)) * velocidad.x * Time.deltaTime);
                if(paredTransform.position.x -15 < _transform.position.x) {
                    pared.SetActive(false);
                    if (_rocksClipPlayed < 1)
                    {
                        SoundManager.Instance.PlayEffectSound(_rocksClip);
                    }
                    _rocksClipPlayed++;
                }
                if (_transform.position.x > posiciónDeseada.x) {
                    PlayerAccess.Instance.Input.enabled = true;
                    Destroy(gameObject);
                }
                break;
        }
    }
}
