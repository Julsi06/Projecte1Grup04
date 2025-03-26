using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float offsetZ = -10f; // La distancia fija que deseas en el eje Z

    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = player.position.x; // Sigue al jugador en el eje X
        position.y = player.position.y; // Sigue al jugador en el eje Y
        position.z = offsetZ; // Mantén la cámara fija en el eje Z

        transform.position = position;
    }
}