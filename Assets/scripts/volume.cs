using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute; // Corregido el nombre y tipo de variable

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f); // Corregido "PlayerPrefers" a "PlayerPrefs"
        sliderValue = slider.value; // Asegurar que sliderValue esté sincronizado
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute(); // Corregido el nombre del método para seguir convenciones
    }

    public void ChangeSlider(float value) // Método faltante para manejar cambios en el slider
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = sliderValue;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute() // Corregido el nombre del método
    {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }
}