using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private HealthScriptableObject healthScriptableObject;
    private int maxHealth;
    private int currentHealth;

    void Start()
    {
        ApplyConfig(healthScriptableObject);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(int hpToheal)
    {
        currentHealth += hpToheal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void ApplyConfig(HealthScriptableObject healthConfig)
    {
        this.maxHealth = healthConfig.MaxHealth;
        this.currentHealth = healthConfig.StartingHealth;
    }
}
