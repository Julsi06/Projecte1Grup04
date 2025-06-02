using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    public TMP_Text mainText; // Texto principal donde se acumularán las líneas
    public TMP_Text pressEnterText; // Texto "Press Enter to continue"
    public AudioClip staticSound; // Sonido de electricidad estática
    private AudioSource audioSource;

    [SerializeField, TextArea(2, 20)] private string[] introLines; // Las líneas de texto
    private bool isTyping = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = staticSound;
        audioSource.loop = true;
        audioSource.Play();

        pressEnterText.gameObject.SetActive(false);
        mainText.text = string.Empty;

        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        foreach (string line in introLines)
        {
            yield return StartCoroutine(TypeLine(line));
            mainText.text += "\n"; // Salto de línea para separar cada frase
            yield return new WaitForSeconds(0.5f); // Breve pausa antes de la siguiente línea
        }

        audioSource.Stop();
        pressEnterText.gameObject.SetActive(true);

        // Esperar a que el jugador presione Enter para continuar
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        SceneManager.LoadScene("Level_1"); // Cambia por la escena real
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;

        foreach (char c in line)
        {
            mainText.text += c;
            yield return new WaitForSeconds(0.03f); // Velocidad de escritura (puedes ajustar)
        }

        isTyping = false;
    }
}
