
public interface IHealth
{
    public void TakeDamage(int damage);

    public void Heal(int hpToheal);

    public void Die();

    public void ApplyConfig(HealthScriptableObject healthConfig);
}
