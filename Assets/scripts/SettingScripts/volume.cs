using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer mixer; // Ya no es público, se carga automáticamente

    private void Start()
    {
        volumeSlider.minValue = 0.0001f;
        volumeSlider.maxValue = 0.9f;

        float savedVolume = PlayerPrefs.GetFloat("volume", 0.5f);

        // Inicializar slider con valor guardado
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        // Guardar el nuevo brillo en el singleton
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();

        float dB = Mathf.Log10(Mathf.Clamp(value, volumeSlider.minValue, volumeSlider.maxValue)) * 20f;
        mixer.SetFloat("VolumeMaster", dB);
    }
}
