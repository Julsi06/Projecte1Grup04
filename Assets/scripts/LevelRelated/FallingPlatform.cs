using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Time parameters for platforms
    private float fallDelay = 5f;
    private float respawnDelay = 5f;
    private Vector2 originalPos;
    private bool playedOnce;
    // Giving the platform a rigidbody so we can manipulate its states
    [SerializeField] private Rigidbody2D platform;

    private void Start()
    {
        platform = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playedOnce)
            {
                StartCoroutine(Fall());
            }
            else
            {
                StopCoroutine(Fall());
            }
            playedOnce = false;
        }
    }
 
    // Used to control the time and the platform's states
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        // Transforms platform's rigidbody from static to dynamic
        platform.bodyType = RigidbodyType2D.Dynamic;
        playedOnce = true;
        yield return new WaitForSeconds(respawnDelay);
        // Transforms platform's rigidbody from dynamic to static
        platform.bodyType = RigidbodyType2D.Static;
        // Respawning to original position
        transform.position = originalPos;
    }

}
