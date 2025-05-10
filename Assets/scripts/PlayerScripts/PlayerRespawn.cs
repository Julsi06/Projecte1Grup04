using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnDelay = 0.5f;

    public void Respawn()
    {
        Vector3 spawn = SpawnManager.Instance.GetSpawnPoint();

        if (spawn != Vector3.zero)
        {
            gameObject.SetActive(true); // <- reactiva el jugador
            transform.position = spawn;
            Debug.Log("Jugador reapareció en: " + spawn);
        }
        else
        {
            Debug.LogError("No se ha establecido un punto de spawn válido.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
}
