using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 100;

    public void Heal(int amount)
    {
        currentHealth += amount;
        Debug.Log("Vida actual: " + currentHealth);
    }
}
public class animateItems : MonoBehaviour
{
    public int healAmount = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth currentHealth = other.GetComponent<PlayerHealth>();
            if (currentHealth !=null) 
            {
                currentHealth.Heal(healAmount);
                Debug.Log(healAmount + " de vida added");
            }

            Destroy(gameObject); // Elimina el ítem
        }
    }
}


