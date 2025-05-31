using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider brilloSlider;
    public Slider volumenSlider;

    void Start()
    {
        // Leer valores previos y aplicar a la UI
        brilloSlider.value = PlayerPrefs.GetFloat("Brillo", 1.0f);
        volumenSlider.value = PlayerPrefs.GetFloat("Volumen", 0.5f);
    }

    public void OnBrilloChange(float valor)
    {
        PlayerPrefs.SetFloat("Brillo", valor);
    }

    public void OnVolumenChange(float valor)
    {
        PlayerPrefs.SetFloat("Volumen", valor);
    }

    public void GuardarYSalir()
    {
        PlayerPrefs.Save();
    }
}
