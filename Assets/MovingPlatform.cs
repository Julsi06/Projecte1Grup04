using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Parameters for the platforms
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;

    private bool waitingPlatform = false;
    private bool isMoving = false; // Nueva variable para controlar el movimiento
    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = pointB.position;
    }

    // Manages the platforms' movement and the direction of the journey that they do
    private void Update()
    {
        // No hace nada si la plataforma está esperando o no está activa
        if (waitingPlatform || !isMoving)
            return;

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        // Checks which point is next
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
            StartCoroutine(WaitAndContinue());
        }
    }

    private IEnumerator WaitAndContinue()
    {
        // Tell the platform to wait
        waitingPlatform = true;
        yield return new WaitForSeconds(stopTime);
        // And then continue
        waitingPlatform = false;
    }

    // Detecta cuando algo colisiona con la plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar que el objeto que colisionó tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = true; // Activa el movimiento
        }
    }
}
