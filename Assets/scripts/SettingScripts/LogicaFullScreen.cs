using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar el componente Toggle

public class LogicaFullScreen : MonoBehaviour
{
    public Toggle toggle; // casilla pantalla completa 

    // Start is called before the first frame update
    void Start()
    {
        // dependiendo de cómo esté la pantalla al iniciar diremos si está on o no el botón
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta; // Corregido: usar Screen en lugar de LogicaFullScreen
    }
}