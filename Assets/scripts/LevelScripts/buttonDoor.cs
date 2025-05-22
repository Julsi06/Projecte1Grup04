using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class buttonDoor : MonoBehaviour
{
    public DoorOpenBehaviour door;
    public DoorCloseBehaviour closeDoor;
    public CameraFollows cameraFollows;
    public DeactivateCollider invisibleWall;
    public Light2D lightToDeactivate1;
    public Light2D lightToDeactivate2;

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
                cameraFollows.MoveCameraToDoor(door.transform, () =>
                {
                    door.OpenDoor();
                    StartCoroutine(DisableLightAfterDelay(0.7f));
                });
            }

            if (closeDoor != null)
            {
                closeDoor.CloseDoor();
            }

            if (invisibleWall != null)
            {
                invisibleWall.OpenWall();
            }
        }
    }

    private IEnumerator DisableLightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (lightToDeactivate1 != null)
        {
            lightToDeactivate1.enabled = false;
            lightToDeactivate2.enabled = false;
        }
    }
}
