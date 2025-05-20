using UnityEngine;
using System.Linq;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject spider;
    [SerializeField] private float tiempoEnemigos = 2f;
    [SerializeField] private Transform playerTransform;

    private float minX, maxX, minY, maxY;
    private float timeNextEnemy;

    private void Start()
    {
        if (puntos == null || puntos.Length == 0)
        {
            Debug.LogError("¡Asigna puntos en el Inspector!");
            return;
        }
        CalcularLimites();
    }

    private void CalcularLimites()
    {
        minX = puntos.Min(punto => punto.position.x);
        maxX = puntos.Max(punto => punto.position.x);
        minY = puntos.Min(punto => punto.position.y);
        maxY = puntos.Max(punto => punto.position.y);
    }

    private void Update()
    {
        timeNextEnemy += Time.deltaTime;
        if (timeNextEnemy >= tiempoEnemigos)
        {
            timeNextEnemy = 0;
            CrearEnemigo();
        }
    }

    private void CrearEnemigo()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0
        );
        GameObject instanceReference = Instantiate(spider, randomPosition, Quaternion.identity);
        instanceReference.GetComponent<spiderMovement>().SetPlayer(playerTransform);
    }
}