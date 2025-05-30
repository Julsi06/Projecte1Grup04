using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessSlide : MonoBehaviour
{
    public Slider brightnessSlider;

    private void Start()
    {
        brightnessSlider.minValue = 0;
        brightnessSlider.maxValue = 0.9f;

        float savedBrightness = PlayerPrefs.GetFloat("brightness", 0);
        if (savedBrightness == 0)
        {
            savedBrightness = 0.5f;
        }

        // Inicializar slider con valor guardado
        brightnessSlider.value = savedBrightness;

        brightnessSlider.onValueChanged.AddListener(OnSliderChanged);
    
    }

    private void OnSliderChanged(float value)
    {
        // Guardar el nuevo brillo en el singleton
        SettingsManager.Instance.brightness = value;
        Debug.Log(value);
        PlayerPrefs.SetFloat("brightness", value);
        PlayerPrefs.Save();
    }
}
