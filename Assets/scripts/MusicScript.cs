using System.Collections;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip musicaSalto;     // Asigna en el Inspector
    public AudioClip muerteMusica;    // Asigna en el Inspector

    private AudioSource musicaFondo;
    private AudioSource efectosAudio;

    private bool haMuerto = false;

    void Start()
    {
        // Música de fondo
        musicaFondo = GetComponent<AudioSource>();
        musicaFondo.loop = true;
        musicaFondo.Play();

        // Audio para salto y muerte (compartido)
        efectosAudio = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Reproducir música de salto si no ha muerto
        if (Input.GetKeyDown(KeyCode.Space) && !haMuerto)
        {
            PlayFX(musicaSalto);

            //StartCoroutine(ReproducirEfectoTemporal(musicaSalto));

        }
    }

    // Llama a esta función desde otro script cuando el jugador muere
    public void ReproducirMusicaMuerte()
    {
        if (!haMuerto)
        {
            haMuerto = true;
            musicaFondo.Pause();
            efectosAudio.clip = muerteMusica;
            efectosAudio.Play();
        }
    }

    private void PlayFX(AudioClip clip)
    {
        efectosAudio.clip = clip;
        efectosAudio.Play();
    }

    // Corrutina que pausa la música de fondo, reproduce un efecto, y la reanuda
    IEnumerator ReproducirEfectoTemporal(AudioClip clip)
    {
        musicaFondo.Pause();

        efectosAudio.clip = clip;
        efectosAudio.Play();

        yield return new WaitForSeconds(clip.length);

        if (!haMuerto)
        {
            musicaFondo.UnPause();
        }
    }
}