using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth
{
    public UnityEvent Died = new();
    public UnityEvent TreeHealthChanged = new();

    [SerializeField] bool ebnableDebugLog;
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
        TreeHealthChanged.Invoke();
    }

    public void Die()
    {
        Died.Invoke();
        Destroy(gameObject);
    }

    public void Heal(int hpToheal)
    {
        currentHealth += hpToheal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        TreeHealthChanged.Invoke();
    }

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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (ebnableDebugLog)
        {
            Debug.Log($"Took damge, new healt: {currentHealth}");
        }
        if (currentHealth < 0)
        {
            Die();
        }

        TreeHealthChanged.Invoke();
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
