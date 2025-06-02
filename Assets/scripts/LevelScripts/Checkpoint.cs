using System.Collections;
using UnityEngine;
using TMPro; // Asegúrate de incluir esto para usar TextMesh Pro

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    public TMP_Text messageText; // Referencia al componente TMP_Text
    private bool messageShown = false; // Para que el mensaje se muestre solo la primera vez
    private bool audioSourceShown = false; // Para evitar reproducir el sonido varias veces

    public AudioClip checkpointSound; // Clip de audio para el sonido del checkpoint
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Asegúrate de tener un AudioSource
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnManager.Instance.SetSpawnPoint(transform.position);
            animator.SetTrigger("Activated");
            if (!messageShown)
            {
                ShowMessage("Checkpoint Activated!"); // Muestra el mensaje solo la primera vez
                messageShown = true; // Marca que el mensaje ya fue mostrado
            }

            // Reproducir el sonido del checkpoint
            if (checkpointSound != null && !audioSourceShown)
            {
                audioSource.PlayOneShot(checkpointSound);
                audioSourceShown = true;
            }
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message; // Asigna el mensaje al texto
        StartCoroutine(ClearMessage()); // Llama a la coroutine para limpiar el mensaje después de un tiempo
    }

    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2); // Espera 2 segundos
        messageText.text = ""; // Limpia el mensaje
    }
}
