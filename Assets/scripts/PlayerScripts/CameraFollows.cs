using System.Collections;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform player;
    public float offsetZ = -10f;
    public float offsetY = 1.5f;
    private float moveSpeed = 10f;
    public float waitTime = 2f;

    private Coroutine moveCoroutine;
    private PlayerMovement playerMovement; // Referencia para bloquear movimiento

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        if (moveCoroutine == null)
        {
            if (playerMovement != null)
                playerMovement.canMove = true;

            Vector3 pos = transform.position;
            pos.x = player.position.x;
            pos.y = player.position.y + offsetY;
            pos.z = offsetZ;

            transform.position = pos;

            // Permitir que el jugador se mueva normalmente
            if (playerMovement != null)
                playerMovement.canMove = true;
        }
        else
        {
            if (playerMovement != null)
                playerMovement.canMove = false;

            // Cuando la cámara se mueve, bloqueamos el movimiento
            if (playerMovement != null)
                playerMovement.canMove = false;
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

        // Volver a la posición original (jugador)
        while (Vector3.Distance(transform.position, originalPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        moveCoroutine = null;
    }
}
