using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private Text counterText; // Arrastra el objeto de texto de la cuenta aquí desde el Inspector

    [SerializeField]
    private int counter = 0; // Valor inicial del contador

    [SerializeField]
    private float countSpeed = 1f; // Velocidad de conteo en incrementos por segundo

    private float elapsedTime = 0f;

    void Update()
    {
        // Incrementar el contador basándonos en el tiempo que ha pasado
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f / countSpeed)
        {
            counter++;
            UpdateCounterText();
            elapsedTime = 0f; // Reiniciar el tiempo transcurrido después de incrementar
        }
    }

    void UpdateCounterText()
    {
        // Mostrar el valor del contador en el UI
        counterText.text = counter.ToString();
    }
}
