using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    private Vector3 currentSpawnPoint;

    [Header("Configuración de Cámara")]
    public Camera mainCamera;
    public float cameraFollowSpeed = 20f; // Velocidad de seguimiento rápido

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Si no se asigna la cámara, buscarla automáticamente
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;
    }

    public Vector3 GetSpawnPoint()
    {
        return currentSpawnPoint;
    }

    public void Respawn()
    {
        Vector3 spawn = SpawnManager.Instance.GetSpawnPoint();
        if (spawn != Vector3.zero)
        {
            transform.position = spawn;

            // Hacer que la cámara siga rápidamente al jugador
            StartCoroutine(QuickCameraFollow(spawn));

            Debug.Log("Jugador reapareció en: " + spawn);
        }
        else
        {
            Debug.LogError("No se ha establecido un punto de spawn válido.");
        }
    }

    private IEnumerator QuickCameraFollow(Vector3 targetPosition)
    {
        if (mainCamera == null) yield break;

        Vector3 targetCameraPosition = new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.z);

        while (Vector3.Distance(mainCamera.transform.position, targetCameraPosition) > 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetCameraPosition,
                cameraFollowSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Asegurar que la cámara llegue exactamente a la posición final
        mainCamera.transform.position = targetCameraPosition;
    }
}