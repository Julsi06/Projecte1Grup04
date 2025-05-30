using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSettingsApplier : MonoBehaviour
{
    public AudioMixer audioMixer; // Asigna tu AudioMixer en el Inspector
    void Start()
    {
        // Leer valores guardados
        float volumen = PlayerPrefs.GetFloat("Volumen", 0.5f);
        float brillo = PlayerPrefs.GetFloat("Brillo", 1.0f);

        // Aplicar volumen (asumiendo que tienes un parámetro "Volume" en el AudioMixer)
        audioMixer.SetFloat("Volume", Mathf.Log10(volumen <= 0.01f ? 0.01f : volumen) * 20f);

        // Aplicar brillo (aquí solo un ejemplo básico)
        RenderSettings.ambientIntensity = brillo; // Necesita una luz ambiental o puedes usar postprocesado
    }
}
