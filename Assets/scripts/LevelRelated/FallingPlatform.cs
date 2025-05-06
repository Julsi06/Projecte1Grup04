using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Time parameters for platforms
    private float fallDelay = 8f;
    private float destroyDelay = 2f;
    // Giving the platform a rigidbody so we can manipulate its states
    [SerializeField] private Rigidbody2D platform;

    private void Start()
    {
        platform = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
 
    // Used to control the time and the platform's states
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        // Transforms platform's rigidbody from kinematic to dynamic
        platform.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
