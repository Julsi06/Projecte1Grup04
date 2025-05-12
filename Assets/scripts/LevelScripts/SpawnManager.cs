using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    private Vector3 currentSpawnPoint;

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
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;
        Debug.Log("Nuevo punto de spawn guardado en: " + currentSpawnPoint);
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
            Debug.Log("Jugador reapareció en: " + spawn);
        }
        else
        {
            Debug.LogError("No se ha establecido un punto de spawn válido.");
        }
    }
}
