using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;


    // Start is called before the first frame update
    void Start()
    {
        //valor para que se quede guardado, el ultimo brillo configuarado antes de salirt del juego o etc
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

       
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, slider.value);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }
}
