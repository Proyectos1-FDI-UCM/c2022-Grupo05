using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
    
    public UnityAction accionEntrar, accionSalir;

    private void OnTriggerEnter2D(Collider2D collision) {
        accionEntrar();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        accionSalir();
    }
}
