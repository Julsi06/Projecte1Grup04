using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float offsetZ = -10f; // La distancia fija que deseas en el eje Z

    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = player.position.x + 2; // Sigue al jugador en el eje X
        position.y = player.position.y; // Sigue al jugador en el eje Y
        position.z = offsetZ; // Mant�n la c�mara fija en el eje Z

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Destroy(gameObject);
    }
}

