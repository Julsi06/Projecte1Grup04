using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;

    void Start()
    {
        // Cargar el valor guardado del brillo (alpha del panel)
        sliderValue = PlayerPrefs.GetFloat("brillo", 0.5f);
        slider.value = sliderValue;

        // Aplicar el valor al alpha del panel (dejando los colores r, g, b intactos)
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        // Actualizar solo el alpha (componente 'a') del color del panel
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }
}