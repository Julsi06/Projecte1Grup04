using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 1; // Daño que inflige el ataque

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy")) // Asegúrate que tus enemigos tengan el tag "Enemy"
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Boss boss = collision.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if(boss != null)
            {
                boss.TakeDamage(damage);
            }

            if (collision.CompareTag("enemyBullet"))
            {
                Destroy(collision.gameObject); // Destruye la bala enemiga
            }
            if (collision.CompareTag("bossBullet")) 
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
