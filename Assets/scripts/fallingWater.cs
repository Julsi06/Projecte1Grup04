using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fallingWater : MonoBehaviour
{
    private BoxCollider2D toxicWater;

    private void Awake()
    {
        toxicWater = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("fallingWater"))
        {
            if (Input.GetKey(KeyCode.P))
            {
                ;
            }
            else
            {
                Death();
            }
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
}
