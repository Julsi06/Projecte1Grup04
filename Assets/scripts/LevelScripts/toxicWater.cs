using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toxicWater : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream:Assets/scripts/LevelScripts/toxicWater.cs
        if (collision.gameObject.CompareTag("toxicWater"))
=======
        if (collision.gameObject.CompareTag("ToxicWater"))
        {
>>>>>>> Stashed changes:Assets/scripts/toxicWater.cs
            Death();
    }

    private void Death()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
