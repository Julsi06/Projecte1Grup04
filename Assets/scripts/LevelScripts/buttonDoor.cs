using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonDoor : MonoBehaviour
{
    public DoorOpenBehaviour door;
    public DoorCloseBehaviour closeDoor;

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

            if (closeDoor != null)
            {
                Debug.Log("Cerrando puerta...");
                closeDoor.CloseDoor();
            }
        }
    }
}
