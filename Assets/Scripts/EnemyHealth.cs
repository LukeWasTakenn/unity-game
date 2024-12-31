using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    
    private IDamageable damageable;

    void Awake()
    {
        health = maxHealth;
        damageable = GetComponent<IDamageable>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        damageable.OnTakeDamage();
        
        if (health <= 0)
        {
            damageable.OnDeath();
        }
    }
}
