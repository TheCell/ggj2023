using System;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour, IAttack
{
    public UnityEvent TargetDestroyed = new();

    [SerializeField] private AttackConfig AttackScriptableObject;
    private int AttackDamage;
    private float TimeBetweenAttacks;
    private float lastAttackTime;
    private Health target;
    private Boolean hasActiveTarget = false;

    void Start()
    {
        ApplyConfig(AttackScriptableObject);
    }

    public void Update()
    {
        if (!hasActiveTarget)
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
        hasActiveTarget = true;
        lastAttackTime = Time.time;
        this.target = target;
    }

    public void ResetTarget()
    {
        this.target = null;
    }

    public void AttackTarget()
    {
        if (target == null)
        {
            Debug.Log("no target left");
            ResetTarget();
            hasActiveTarget = false;
            TargetDestroyed.Invoke();
            return;
        }
        else
        {
            target.TakeDamage(AttackDamage);
        }
    }
}
