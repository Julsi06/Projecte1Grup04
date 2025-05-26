using System.Collections;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform player;
    public float offsetZ = -10f;
    public float offsetY = 1.5f;
    private float moveSpeed = 18f;
    public float waitTime = 2f;

    private Coroutine moveCoroutine;
    private PlayerMovement playerMovement; // Referencia para bloquear movimiento

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        if (playerMovement == null)
            return;

        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();

        if (moveCoroutine == null)
        {
            // Cámara está sobre el jugador: dejar moverse libremente
            playerMovement.canMove = true;

            if (rb2d != null)
            {
                rb2d.bodyType = RigidbodyType2D.Dynamic;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation; // Quitar cualquier freeze
            }

            Vector3 pos = new Vector3(player.position.x, player.position.y + offsetY, offsetZ);
            transform.position = pos;
        }
        else
        {
            // Cámara está moviéndose a la puerta o lejos del jugador: congelar jugador
            playerMovement.canMove = false;

            if (rb2d != null)
            {
                rb2d.velocity = Vector2.zero;
                rb2d.angularVelocity = 0f;
                rb2d.bodyType = RigidbodyType2D.Kinematic;

                // Congelar posición X e Y y rotación para que no se mueva ni caiga
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    // Nuevo método con callback para activar la animación de la puerta
    public void MoveCameraToDoor(Transform door, System.Action onArriveDoor)
    {
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveToDoorSequence(door, onArriveDoor));
        }
    }

    private IEnumerator MoveToDoorSequence(Transform door, System.Action onArriveDoor)
    {
        Vector3 originalPos = new Vector3(player.position.x, player.position.y + offsetY, offsetZ);
        Vector3 doorPos = new Vector3(door.position.x, door.position.y + offsetY, offsetZ);

        // Mover cámara a la puerta
        while (Vector3.Distance(transform.position, doorPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Activar animación de la puerta cuando llegue
        onArriveDoor?.Invoke();

        // Esperar el tiempo para mostrar la puerta abierta
        yield return new WaitForSeconds(waitTime);

        yield return null;

        moveCoroutine = null;
    }
}
