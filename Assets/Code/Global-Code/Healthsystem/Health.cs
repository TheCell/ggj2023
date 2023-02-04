using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private HealthScriptableObject healthScriptableObject;
    private int maxHealth;
    private int currentHealth;
    private int startingHealth;
    private int tickDamageRate;
    private int tickDamageAmount;

    private int fixedUpdateCounter = 0;

    void Start()
    {
        ApplyConfig(healthScriptableObject);
    }

    public void Die()
    {
        if (GetComponent<Tree>())
        {
            this.transform.parent.GetComponent<Crossroad>().DestroyTree();
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int hpToheal)
    {
        currentHealth += hpToheal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tickDamageAmount + tickDamageRate > 0 && fixedUpdateCounter >= tickDamageRate)
        {
            this.GetComponent<Health>().TickDamage();
            this.GetComponent<Tree>().SetSize();

            fixedUpdateCounter = 0;
        }
        else
        {
            fixedUpdateCounter++;
        }
    }

    public void TickDamage()
    {
        TakeDamage(tickDamageAmount);
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
        this.startingHealth = healthConfig.StartingHealth;
        this.tickDamageRate = healthConfig.TickDamageRate;
        this.tickDamageAmount = healthConfig.TickDamageAmount;
    }

    public int getCurrentHealth() { return currentHealth; }
    public int getStartingHealth() { return startingHealth; }
}
