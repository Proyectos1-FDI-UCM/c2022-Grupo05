using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransitionAnimation : MonoBehaviour
{
    [SerializeField] Vector2 posiciónDeseada;
    [SerializeField] Vector2 velocidad = new Vector2(1, 1);
    [SerializeField] float velocidadRetroceso = 1;
    [SerializeField] GameObject jefeSegundaFase;
    [SerializeField] GameObject pared; 

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Transform paredTransform;

    private uint state = 0;
    private float cont;
    float unPocoALaIzquierda;

    private void Start() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.simulated = false;
        cont = 0;
        paredTransform = pared.transform;
        PlayerAccess.Instance.Input.enabled = false;
        PlayerAccess.Instance.Movement.Move(Vector2.zero);
        PlayerAccess.Instance.Animation.Run(false);
        PlayerAccess.Instance.Animation.OffShot();
    }

    private void Update() {
        switch(state) {
            case 0:
                cont += Time.deltaTime;
                if(cont >= 0.1f) {
                    state++;
                    _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                }
                break;
            case 1:
                _transform.Translate(Vector3.Normalize(new Vector3(0, posiciónDeseada.y - _transform.position.y, 0)) * velocidad.y * Time.deltaTime);
                if(Mathf.Abs(_transform.position.y - posiciónDeseada.y) < 0.005) {
                    state++;
                    cont = 0;
                }
                break;
            case 2:
                cont += Time.deltaTime;
                if(cont >= 0.25f) {
                    state++;
                    unPocoALaIzquierda = _transform.position.x - 1;
                }
                break;
            case 3:
                _transform.Translate(Vector3.Normalize(new Vector3(unPocoALaIzquierda - _transform.position.x, 0)) * velocidadRetroceso * Time.deltaTime);
                if(Mathf.Abs(_transform.position.x - unPocoALaIzquierda) < 0.005) {
                    state++;
                }
                break;
            case 4:
                _transform.Translate(Vector3.Normalize(new Vector3(posiciónDeseada.x - _transform.position.x, 0, 0)) * velocidad.x * Time.deltaTime);
                Debug.Log(4);
                if(Mathf.Abs(paredTransform.position.x - _transform.position.x) < 0.05) {
                    Debug.Log("Hecho");
                    pared.SetActive(false);
                }
                if(Mathf.Abs(_transform.position.x - posiciónDeseada.x) < 0.005) {
                    PlayerAccess.Instance.Input.enabled = true;
                    jefeSegundaFase.SetActive(true);
                    Destroy(gameObject);
                }
                break;
        }
    }
}
