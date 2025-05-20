using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RobotDialogueSystem : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    public GameObject robotObject;       // Modelo 3D/2D del robot
    public GameObject dialoguePanel;     // Panel UI del diálogo
    public TextMeshProUGUI dialogueText; // Componente de texto
    public Image dialogueImage; // Imagen del diálogo (opcional)

    [Header("Configuración de Activación")]
    public Transform activationPoint;    // Punto de activación
    public float activationRadius = 5f;  // Radio de activación
    public string message = "Bienvenido, para mover al personaje utilice las teclas A y D";

    private Transform player;
    private bool isDialogueActive = false;

    void Start()
    {
        // Validar referencias
        if (robotObject == null || dialoguePanel == null || dialogueText == null || activationPoint == null)
        {
            Debug.LogError("¡Faltan asignaciones en el inspector!", this);
            enabled = false;
            return;
        }

        // Obtener referencia al jugador
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("No se encontró objeto con tag 'Player'", this);
            enabled = false;
            return;
        }

        // Ocultar todos los elementos al inicio
        SetDialogueVisibility(false);
    }

    void Update()
    {
        // Verificar activación por proximidad
        if (!isDialogueActive && Vector3.Distance(player.position, activationPoint.position) <= activationRadius)
        {
            ActivateDialogue();
        }

        // Verificar desactivación por tecla
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DeactivateDialogue();
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
        robotObject.SetActive(visible);
        dialoguePanel.SetActive(visible);
        dialogueText.gameObject.SetActive(visible);

        if (dialogueImage != null)
            dialogueImage.gameObject.SetActive(visible);
    }


    // Visualización del área de activación en el editor
    void OnDrawGizmosSelected()
    {
        if (activationPoint != null)
        {
            Gizmos.color = new Color(0, 1, 1, 0.3f);
            Gizmos.DrawSphere(activationPoint.position, activationRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(activationPoint.position, activationRadius);
        }
    }
}