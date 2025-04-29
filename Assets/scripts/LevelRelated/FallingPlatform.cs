using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Time parameters for platforms
    private float fallDelay = 0.5f;
    private float destroyDelay = 5f;

    // Giving the platform a rigidbody so we can manipulate its states
    [SerializeField] private Rigidbody2D platform;

    // Checks for collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            StartCoroutine(Fall());
    }

    // Used to control the time and the platform's states
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        // Transforms platform's rigidbody from kinematic to dynamic
        platform.bodyType = RigidbodyType2D.Dynamic;
        // Will destroy the object in a given time
        Destroy(gameObject, destroyDelay);
    }
}
