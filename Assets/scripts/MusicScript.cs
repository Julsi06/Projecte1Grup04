using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    public AudioClip musicaFondoClip;   // Música de fondo
    public AudioClip musicaSalto;       // Efecto de salto
    public AudioClip muerteMusica;      // Música al morir
    public AudioClip andarClip;         // Sonido de pasos

    public AudioMixer audioMixer;       // Asignar el AudioMixer desde el Inspector

    private AudioSource musicaFondo;
    private AudioSource efectosAudio;
    private AudioSource pasosAudio;

    private bool haMuerto = false;

    void Start()
    {
        // Música de fondo
        musicaFondo = gameObject.AddComponent<AudioSource>();
        musicaFondo.clip = musicaFondoClip;
        musicaFondo.loop = true;
        musicaFondo.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        musicaFondo.Play();

        // Efectos (salto, muerte)
        efectosAudio = gameObject.AddComponent<AudioSource>();
        efectosAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("FX")[0];

        // Pasos
        pasosAudio = gameObject.AddComponent<AudioSource>();
        pasosAudio.clip = andarClip;
        pasosAudio.loop = true;
        pasosAudio.playOnAwake = false; // importante: no reproducir al inicio
        pasosAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("FX")[0];
    }

    void Update()
    {
        if (haMuerto) return;

        // Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayFX(musicaSalto);
        }

        // Detectar teclas de andar
        bool pulsandoAndar = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                             Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        // Reproducir pasos solo al moverse
        if (pulsandoAndar && !pasosAudio.isPlaying)
        {
            pasosAudio.Play();
        }
        else if (!pulsandoAndar && pasosAudio.isPlaying)
        {
            pasosAudio.Stop();
        }
    }

    public void ReproducirMusicaMuerte()
    {
        if (!haMuerto)
        {
            haMuerto = true;
            musicaFondo.Pause();
            pasosAudio.Stop();
            PlayFX(muerteMusica);
        }
    }

    private void PlayFX(AudioClip clip)
    {
        efectosAudio.Stop();
        efectosAudio.clip = clip;
        efectosAudio.Play();
    }

    // Control logarítmico de volumen de música
    public void SetVolumenMusica(float valorLineal)
    {
        float dB = Mathf.Log10(Mathf.Clamp(valorLineal, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("VolumenMusica", dB);
    }

    // Control logarítmico de volumen de efectos
    public void SetVolumenFX(float valorLineal)
    {
        float dB = Mathf.Log10(Mathf.Clamp(valorLineal, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("VolumenFX", dB);
    }
}
