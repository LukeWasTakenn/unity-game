using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    
    private IDamageable damageable;
    public HealthBar healthBar;
    
    void Awake()
    {
        health = maxHealth;
        damageable = GetComponent<IDamageable>();
        
        if (healthBar)
            healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        damageable.OnTakeDamage();
        
        if (health <= 0)
            health = 0;
        
        Debug.Log(healthBar);
        if (healthBar)
            healthBar.SetHealth(health);
        
        if (health <= 0)
        {
            damageable.OnDeath();
        }
    }
}
