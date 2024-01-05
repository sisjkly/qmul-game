// EnemyHealth script
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public HealthBarControl healthBarControl;  // Reference to the HealthBarControl script

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthBarControl.SetHealth(currentHealth, maxHealth);  // Updated call to SetHealth, passing both currentHealth and maxHealth
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
