public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                lastAttackTime = Time.time;
            }
        }
    }
}