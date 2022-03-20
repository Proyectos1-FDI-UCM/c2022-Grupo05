using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransitionAnimation : MonoBehaviour
{
    [SerializeField] Vector2 posiciónDeseada;
    [SerializeField] Vector2 velocidad = new Vector2(1, 1);
    [SerializeField] float velocidadRetroceso = 1;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private uint state = 0;
    private float cont;
    float unPocoALaIzquierda;

    private void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.simulated = false;
        _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        PlayerAccess.Instance.Input.enabled = false;
        PlayerAccess.Instance.Movement.Move(Vector2.zero);
        PlayerAccess.Instance.Animation.Run(false);
    }

    private void Update() {
        switch(state) {
            case 0:
                _transform.Translate(Vector3.Normalize(new Vector3(0, posiciónDeseada.y - _transform.position.y, 0)) * velocidad.y * Time.deltaTime);
                if(Mathf.Abs(_transform.position.y - posiciónDeseada.y) < 0.005) {
                    state++;
                    cont = 0;
                }
                break;
            case 1:
                cont += Time.deltaTime;
                if(cont >= 0.25f) {
                    state++;
                    unPocoALaIzquierda = _transform.position.x - 1;
                }
                break;
            case 2:
                _transform.Translate(Vector3.Normalize(new Vector3(unPocoALaIzquierda - _transform.position.x, 0)) * velocidadRetroceso * Time.deltaTime);
                if(Mathf.Abs(_transform.position.x - unPocoALaIzquierda) < 0.005) {
                    state++;
                }
                break;
            case 3:
                _transform.Translate(Vector3.Normalize(new Vector3(posiciónDeseada.x - _transform.position.x, 0, 0)) * velocidad.x * Time.deltaTime);
                if(Mathf.Abs(_transform.position.x - posiciónDeseada.x) < 0.005) {
                    PlayerAccess.Instance.Input.enabled = true;
                    Debug.Log("Destruido");
                    Destroy(gameObject);
                }
                break;
        }
    }
}
