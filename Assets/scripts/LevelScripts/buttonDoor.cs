using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class buttonDoor : MonoBehaviour
{
    public DoorOpenBehaviour door;
    public DoorOpenBehaviour door2;
    public CameraFollows cameraFollows;
    public Light2D lightToDeactivate1;
    public Light2D lightToDeactivate2;
    public Light2D lightToDeactivate3;
    public Light2D lightToDeactivate4;

    public AudioClip buttonPressSound; // Clip de audio para el sonido del botón
    private AudioSource audioSource;

    private bool isActivated = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Asegúrate de tener un AudioSource
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActivated)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            Debug.Log("Botón activado por el jugador");

            // Reproducir el sonido del botón
            if (buttonPressSound != null)
            {
                audioSource.PlayOneShot(buttonPressSound);
            }

            if (door != null && cameraFollows != null)
            {
                cameraFollows.MoveCameraToDoor(door.transform, () =>
                {
                    door.OpenDoor();
                    StartCoroutine(DisableLightAfterDelay(0.7f));
                });
            }
            if (door2 != null)
            {
                door2.OpenDoor();
            }
        }
    }

    private IEnumerator DisableLightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (lightToDeactivate1 != null && door2 == null)
        {
            lightToDeactivate1.enabled = false;
            lightToDeactivate2.enabled = false;
        }
        if (lightToDeactivate1 != null && door2 != null)
        {
            lightToDeactivate1.enabled = false;
            lightToDeactivate2.enabled = false;
            lightToDeactivate3.enabled = false;
            lightToDeactivate4.enabled = false;
        }
    }
}
