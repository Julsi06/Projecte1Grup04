using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    [Header("Configuración del Portal")]
    public Vector3 destinationPosition = new Vector3(501.68f, -32.82f, 0f);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Mover al jugador a la posición del segundo nivel
            other.transform.position = destinationPosition;

            // Opcional: Si el jugador tiene un Rigidbody2D, detener su velocidad
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero;
            }
        }
    }
}