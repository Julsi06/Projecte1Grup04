using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 1; // Daño que inflige el ataque

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy")) // Asegúrate que tus enemigos tengan el tag "Enemy"
        {
            EnemyBat enemy = collision.GetComponent<EnemyBat>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (collision.CompareTag("enemyBullet"))
            {
                Destroy(collision.gameObject); // Destruye la bala enemiga
            }
        }
    }
}
