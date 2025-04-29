using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip musicaSalto;     // Asigna en el Inspector
    public AudioClip muerteMusica;    // Asigna en el Inspector

    private AudioSource musicaFondo;
    private AudioSource saltoAudio;
    private AudioSource muerteAudio;

    private bool haMuerto = false;

    void Start()
    {
        // Música de fondo
        musicaFondo = GetComponent<AudioSource>();
        musicaFondo.loop = true;
        musicaFondo.Play();

        // Audio de salto
        saltoAudio = gameObject.AddComponent<AudioSource>();

        // Audio de muerte
        muerteAudio = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Reproducir música de salto
        if (Input.GetKeyDown(KeyCode.Space) && !haMuerto)
        {
            saltoAudio.clip = musicaSalto;
            saltoAudio.Play();
        }
    }

    // Llama a esta función cuando el jugador muere
    public void ReproducirMusicaMuerte()
    {
        if (!haMuerto)
        {
            haMuerto = true;

            // Detiene la música de fondo
            musicaFondo.Stop();

            // Reproduce la música de muerte
            muerteAudio.clip = muerteMusica;
            muerteAudio.Play();
        }
    }
}
