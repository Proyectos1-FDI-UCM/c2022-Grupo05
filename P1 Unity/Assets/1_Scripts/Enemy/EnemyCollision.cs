using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCollision : MonoBehaviour
{
    public UnityAction accionEntrar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        accionEntrar();
    }
}
