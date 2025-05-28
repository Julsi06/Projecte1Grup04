using System.Collections;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    private LivesManager livesManager;
    public CameraFollows cameraFollows;  // Referencia al script CameraFollows

    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private PlayerRespawn playerRespawn;

    private void Start()
    {
        livesManager = FindObjectOfType<LivesManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();

        if (cameraFollows == null)
        {
            Debug.LogError("CameraFollows no asignado en PlayerDeathManager.");
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Jugador ha muerto.");

        // Ocultar el sprite
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        // Perder vida (si existe manager)
        if (livesManager != null)
            livesManager.LoseLife();

        // Iniciar secuencia de muerte y respawn
        StartCoroutine(HandleDeathAndRespawn());
    }

    private IEnumerator HandleDeathAndRespawn()
    {
        Vector3 spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        if (spawnPoint == Vector3.zero)
        {
            Debug.LogError("Spawn point inválido en PlayerDeathManager.");
            yield break;
        }

        Debug.Log("Moviendo cámara hacia el spawn point: " + spawnPoint);

        bool arrived = false;

        // Crear un GameObject dummy para usar como target de cámara en spawnPoint
        GameObject dummyTarget = new GameObject("CameraTarget_SpawnPoint");
        dummyTarget.transform.position = spawnPoint;

        // Usar MoveCameraToDoor (renombrado para que signifique mover cámara a punto) con callback
        cameraFollows.MoveCameraToDoor(dummyTarget.transform, () =>
        {
            arrived = true;
        });

        // Esperar hasta que la cámara termine de moverse
        while (!arrived)
        {
            yield return null;
        }

        // Reaparecer jugador en spawn point
        if (playerRespawn != null)
            playerRespawn.Respawn();
        else
            Debug.LogError("PlayerRespawn no encontrado.");

        // Reactivar visual del jugador
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        // Limpiar objeto dummy
        Destroy(dummyTarget);

        isDead = false;

        Debug.Log("Jugador reapareció. Secuencia de muerte finalizada.");
    }
}
