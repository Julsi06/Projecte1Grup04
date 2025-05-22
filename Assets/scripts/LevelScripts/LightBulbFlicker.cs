using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LightBulbFlicker : MonoBehaviour
{
    private Light2D light2D;

    public float minIntensity = 0f;    // Intensidad mínima al apagar
    public float maxIntensity = 1f;    // Intensidad máxima cuando está encendida
    public float flickerDuration = 2f;  // Duración total del parpadeo en segundos
    public float flickerMinTime = 0.05f;  // Tiempo mínimo entre cambios dentro del parpadeo
    public float flickerMaxTime = 0.3f;   // Tiempo máximo entre cambios dentro del parpadeo
    public float delayBetweenFlickers = 10f; // Tiempo que esperará antes de volver a parpadear

    void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogWarning("No hay componente Light2D en este GameObject.");
            enabled = false;
            return;
        }
        StartCoroutine(FlickerCycle());
    }

    IEnumerator FlickerCycle()
    {
        while (true)
        {
            // Espera el delay antes de comenzar el parpadeo
            yield return new WaitForSeconds(delayBetweenFlickers);

            float elapsed = 0f;
            while (elapsed < flickerDuration)
            {
                float waitTime = Random.Range(flickerMinTime, flickerMaxTime);
                yield return new WaitForSeconds(waitTime);

                float intensity = Random.Range(minIntensity, maxIntensity);
                light2D.intensity = intensity;

                elapsed += waitTime;
            }

            // Asegura que la luz vuelva a su intensidad máxima o deseada después del parpadeo
            light2D.intensity = maxIntensity;
        }
    }
}
