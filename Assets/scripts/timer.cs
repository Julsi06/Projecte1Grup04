using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timerText; // Arrastra el objeto de texto de la cuenta aquí desde el Inspector

    [SerializeField]
    private float elapsedTime = 0f; // Tiempo inicial en segundos (comienza en 0)

    void Update()
    {
        elapsedTime += Time.deltaTime; // Incrementar el tiempo conforme pasa cada frame
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // Formatear el tiempo como minutos y segundos
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}