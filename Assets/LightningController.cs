using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    private LivesManager livesManager;
    private PlayerRespawn playerRespawn;
    private Animator animator;
    private Collider2D deathCollider;

    [Header("Duración de la animación")]
    [Tooltip("Tiempo que el rayo permanece activo")]
    [SerializeField] private float activeDuration = 1f;

    [Tooltip("Tiempo que el rayo permanece inactivo")]
    [SerializeField] private float inactiveDuration = 1f;

    [Header("Nombre del parámetro bool en el Animator")]
    [Tooltip("Bool para activar/desactivar el rayo")]
    private string activateBoolName = "activate";

    [Header("Luces 2D a controlar")]
    [Tooltip("Primera luz 2D que se desactivará")]
    public UnityEngine.Rendering.Universal.Light2D light2D1;

    [Tooltip("Segunda luz 2D que se desactivará")]
    public UnityEngine.Rendering.Universal.Light2D light2D2;

    private void Start()
    {
        livesManager = FindObjectOfType<LivesManager>();
        playerRespawn = GetComponent<PlayerRespawn>();
        animator = GetComponent<Animator>();
        deathCollider = GetComponent<Collider2D>();

        if (deathCollider != null)
        {
            deathCollider.enabled = false; // Collider inicialmente desactivado
        }

        // Asegurarse que las luces 2D estén desactivadas inicialmente si están asignadas
        if (light2D1 != null)
            light2D1.enabled = false;

        if (light2D2 != null)
            light2D2.enabled = false;

        StartCoroutine(LightningCycle());
    }

    private IEnumerator LightningCycle()
    {
        while (true)
        {
            // Activar rayo
            animator.SetBool(activateBoolName, true);
            if (deathCollider != null)
            {
                deathCollider.enabled = true;
            }
            if (light2D1 != null)
                light2D1.enabled = true;
            if (light2D2 != null)
                light2D2.enabled = true;

            yield return new WaitForSeconds(activeDuration);

            // Desactivar rayo
            animator.SetBool(activateBoolName, false);
            if (deathCollider != null)
            {
                deathCollider.enabled = false;
            }
            if (light2D1 != null)
                light2D1.enabled = false;
            if (light2D2 != null)
                light2D2.enabled = false;

            yield return new WaitForSeconds(inactiveDuration);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            Death();
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
        playerRespawn.Respawn();
        if (livesManager != null)
        {
            livesManager.LoseLife();
        }
    }
}

