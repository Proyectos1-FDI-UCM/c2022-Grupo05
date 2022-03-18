using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateGravityTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<PlayerMovementManager>() != null) //(CAMBIAR NOMBRE COMPONENTE)
        {
            CinematicManager.Instance.DesactivateGravity();
            Destroy(gameObject);
        }

    }
}
