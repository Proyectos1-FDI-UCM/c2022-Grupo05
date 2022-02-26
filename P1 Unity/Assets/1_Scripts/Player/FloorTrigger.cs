using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorTrigger : MonoBehaviour {
    
    public UnityAction accionEntrar = null, accionSalir = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.isTrigger) accionEntrar?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(!collision.isTrigger) accionSalir?.Invoke();
    }
}
