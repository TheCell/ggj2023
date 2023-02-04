using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private HealthScriptableObject healthScriptableObject;
    private int maxHealth;
    private int currentHealth;
    private int startingHealth;
    private float tickDamageRate;
    private int tickDamageAmount;
    private int tickStableHealth;


    private float lastTickTime;


    void Start()
    {
        ApplyConfig(healthScriptableObject);
        tickDamageRate = tickDamageRate * 0.01f;
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

        TreeCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (tickDamageRate > 0 && Time.time > lastTickTime + tickDamageRate)
        {
            lastTickTime = Time.time;
            TickDamage();
        }
    }

    public void TickDamage()
    {
        int tickFloor = tickStableHealth;
        if (GetComponent<Tree>())
        {
            tickFloor /= 2;
            tickFloor *= transform.parent.GetComponent<Crossroad>().ConnectedTreesAmount();
        }

        if (currentHealth >= tickStableHealth + tickFloor)
        {
            TakeDamage(tickDamageAmount);
        } 
 //       else if (currentHealth >= tickStableHealth)
 //       {
 //           SetHealth(tickStableHealth);
 //       }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }

        TreeCheck();
    }

    private void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth < 0)
        {
            Die();
        }

        TreeCheck();
    }

    private void TreeCheck()
    {
        if (GetComponent<Tree>())
        {
            this.GetComponent<Tree>().SetSize();
        }
    }

    public void ApplyConfig(HealthScriptableObject healthConfig)
    {
        this.maxHealth = healthConfig.MaxHealth;
        this.currentHealth = healthConfig.StartingHealth;
        this.startingHealth = healthConfig.StartingHealth;
        this.tickDamageRate = healthConfig.TickDamageRate;
        this.tickDamageAmount = healthConfig.TickDamageAmount;
        this.tickStableHealth = healthConfig.TickStableHealth;
    }

    public int getCurrentHealth() { return currentHealth; }
    public int getStartingHealth() { return startingHealth; }
}
