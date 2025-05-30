using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de incluir esto para usar TextMesh Pro

public class Chechkpoint : MonoBehaviour
{
    private Animator animator;
    public TMP_Text messageText; // Referencia al componente TMP_Text

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnManager.Instance.SetSpawnPoint(transform.position);
            animator.SetTrigger("Activated");
            ShowMessage("Checkpoint Activated!"); // Muestra el mensaje
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
