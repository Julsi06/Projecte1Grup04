using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonDoor : MonoBehaviour
{
    public DoorBehaviour door;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Botón activado por el jugador");
            if (door != null)
            {
                Debug.Log("Abriendo puerta...");
                door.OpenDoor();
            }
        }
    }
}
