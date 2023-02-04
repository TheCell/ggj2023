using UnityEngine;

public class Attack : MonoBehaviour, IAttack
{
    private int AttackDamage;
    private float TimeBetweenAttacks;
    private Health target;
    private float lastAttackTime;

    public void Update()
    {
        if (target == null)
        {
            return;
        }

        if (Time.time > lastAttackTime + TimeBetweenAttacks)
        {
            lastAttackTime = Time.time;
            AttackTarget();
        }
    }

    public void ApplyConfig(AttackConfig attackConfig)
    {
        AttackDamage = attackConfig.AttackDamage;
        TimeBetweenAttacks = attackConfig.TimeBetweenAttacks;
    }

    public void SetTarget(Health target)
    {
        lastAttackTime = Time.time;
        this.target = target;
    }

    public void ResetTarget()
    {
        this.target = null;
    }

    public void AttackTarget()
    {
        target.TakeDamage(AttackDamage);
    }
}
