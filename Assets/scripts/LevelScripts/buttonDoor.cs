using UnityEngine;

public class buttonDoor : MonoBehaviour
{
    public DoorOpenBehaviour door;
    public DoorCloseBehaviour closeDoor;
    public CameraFollows cameraFollows;

    private bool isActivated = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActivated)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            Debug.Log("Botón activado por el jugador");

            if (door != null && cameraFollows != null)
            {
                // La cámara se mueve hacia la puerta, y cuando llegue, abre la puerta
                cameraFollows.MoveCameraToDoor(door.transform, () =>
                {
                    door.OpenDoor();
                });
            }

            if (closeDoor != null)
            {
                closeDoor.CloseDoor();
            }
        }
    }
}
