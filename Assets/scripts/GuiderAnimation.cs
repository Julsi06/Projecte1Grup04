using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RobotDialogueSystem : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    public GameObject robotObject;       // Modelo 3D/2D del robot
    public GameObject dialoguePanel;     // Panel UI del diálogo
    public TextMeshProUGUI dialogueText; // Componente de texto
    public Image dialogueImage;          // Imagen del diálogo (opcional)
    public GameObject[] additionalObjectsToDisable; // Array de objetos adicionales a desactivar

    [Header("Configuración del Diálogo")]
    public string message = "Bienvenido, para mover al personaje utilice las teclas A y D";

    [Header("Configuración de Teclas")]
    public KeyCode closeKey = KeyCode.Space; // Tecla para cerrar diálogo
    public KeyCode returnKey = KeyCode.Return; // Tecla Intro/Enter
    public bool disableOnAnyKey = false; // Opción para cerrar con cualquier tecla

    private bool isDialogueActive = false;
    private bool canCloseDialogue = false;

    void Start()
    {
        // Validar referencias
        if (dialoguePanel == null || dialogueText == null)
        {
            Debug.LogError("¡Faltan asignaciones esenciales en el inspector!", this);
            enabled = false;
            return;
        }

        SetDialogueVisibility(false);
    }

    void Update()
    {
        // Verificar si se debe cerrar el diálogo
        if (isDialogueActive && canCloseDialogue &&
            (Input.GetKeyDown(closeKey) || Input.GetKeyDown(returnKey) || (disableOnAnyKey && Input.anyKeyDown)))
        {
            DeactivateDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name);

        if (other.CompareTag("Player"))
        {
            ActivateDialogue();
            canCloseDialogue = true;
        }
    }

    void ActivateDialogue()
    {
        isDialogueActive = true;
        dialogueText.text = message;
        SetDialogueVisibility(true);
    }

    void DeactivateDialogue()
    {
        isDialogueActive = false;
        SetDialogueVisibility(false);
    }

    void SetDialogueVisibility(bool visible)
    {

        Debug.Log("Entro");

        // Desactivar/activar todos los objetos del diálogo
        if (robotObject != null)
            robotObject.SetActive(visible);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(visible);

        if (dialogueText != null)
            dialogueText.gameObject.SetActive(visible);

        if (dialogueImage != null)
            dialogueImage.gameObject.SetActive(visible);

        // Desactivar objetos adicionales si existen
        foreach (var obj in additionalObjectsToDisable)
        {
            if (obj != null)
                obj.SetActive(visible);
        }
    }
}