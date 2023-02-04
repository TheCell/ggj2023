
public interface IAttack 
{
    public void SetTarget(Health target);

    public void ResetTarget();
    
    public void AttackTarget();

    public void ApplyConfig(AttackConfig attackConfig);
}
