using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    private AudioMixer mixer; // Ya no es público, se carga automáticamente

    void Awake()
    {
        // Cargar el AudioMixer desde Resources/MainMixer.mixer
        mixer = Resources.Load<AudioMixer>("MainMixer");

        if (mixer == null)
        {
            Debug.LogError("No se pudo cargar el AudioMixer. Asegúrate de que 'MainMixer.mixer' esté en la carpeta Resources.");
        }
    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        sliderValue = slider.value;
        SetVolume(sliderValue);
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        SetVolume(sliderValue);
        RevisarSiEstoyMute();
    }

    private void SetVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("VolumenMaster", dB);
    }

    public void RevisarSiEstoyMute()
    {
        imagenMute.enabled = sliderValue <= 0.0001f;
    }
}
