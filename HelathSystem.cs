using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private UnityEvent onDamaged;
    [SerializeField] private UnityEvent onDeath;

    private int currentHealth;
    private bool isDead = false;

    public int Health => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        
        onDamaged.Invoke();
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        onDeath.Invoke();
        Destroy(gameObject, 1f);
    }
}