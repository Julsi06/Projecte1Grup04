using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject LoadImage;
    private CanvasGroup loadImageCanvasGroup;

    private bool isActive = false;

    [Header("Configuración del Portal")]
    public Vector3 destinationPosition = new Vector3(501.68f, -32.82f, 0f);

    void Start()
    {
        loadImageCanvasGroup = LoadImage.GetComponent<CanvasGroup>();
        if (loadImageCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup component missing on LoadImage object. Please add it for fading effect.");
        }
        LoadImage.SetActive(false);
        loadImageCanvasGroup.alpha = 0f; // ensure transparent at start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive && other.CompareTag("Player"))
        {
            isActive = true;
            StartCoroutine(LoadScreenSequence(other));
        }
    }

    private System.Collections.IEnumerator LoadScreenSequence(Collider2D player)
    {
        LoadImage.SetActive(true); // Activar pantalla de carga

        // Difuminar la pantalla de carga
        yield return StartCoroutine(FadeIn(loadImageCanvasGroup, 1f));

        yield return new WaitForSeconds(1f); // Esperar un momento antes de mover al jugador

        player.transform.position = destinationPosition;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(0.5f); // Esperar un poco más antes de quitar la pantalla de carga

        // Difuminar hacia fuera la pantalla de carga
        yield return StartCoroutine(FadeOut(loadImageCanvasGroup, 1f));

        LoadImage.SetActive(false); // Desactivar pantalla de carga
        isActive = false;
    }

    private System.Collections.IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(time / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private System.Collections.IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (time / duration));
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}

