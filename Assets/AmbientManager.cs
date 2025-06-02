using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public AudioClip ambientClip1; // Primer audio ambiental
    public AudioClip ambientClip2; // Segundo audio ambiental

    public float volumeAmbient1 = 1f; // Volumen del primer audio (más alto)
    public float volumeAmbient2 = 0.5f; // Volumen del segundo audio (más bajo)

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Awake()
    {
        // Crear dos AudioSources en el mismo GameObject para reproducir simultáneamente
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = ambientClip1;
        audioSource1.loop = true;
        audioSource1.volume = volumeAmbient1;
        audioSource1.playOnAwake = false;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = ambientClip2;
        audioSource2.loop = true;
        audioSource2.volume = volumeAmbient2;
        audioSource2.playOnAwake = false;
    }

    void Start()
    {
        if (ambientClip1 != null)
        {
            audioSource1.Play();
        }
        if (ambientClip2 != null)
        {
            audioSource2.Play();
        }
    }

    // Opcional: métodos para controlar el volumen en tiempo real
    public void SetVolumeAmbient1(float volume)
    {
        audioSource1.volume = Mathf.Clamp01(volume);
    }

    public void SetVolumeAmbient2(float volume)
    {
        audioSource2.volume = Mathf.Clamp01(volume);
    }
}

