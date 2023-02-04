using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] float attackRange = 3f;

    private Tree tree;
    private Attack attack;
    private Health health;
    private Crossroad crossroad;

    private bool isAttacking = false;

    void Start()
    {
        attack = GetComponent<Attack>();
        if (attack == null)
        {
            Debug.LogError("Attack not present");
        }

        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("Health not present");
        }

        tree = GetComponent<Tree>();
        if (tree == null)
        {
            Debug.LogError("Tree not present");
        }

        crossroad = GetComponentInParent<Crossroad>();
        if (crossroad == null)
        {
            Debug.LogError("Crossroad not present on Parent");
        }

        attack.TargetDestroyed.AddListener(GetNextTarget);
        health.Died.AddListener(TreeDied);
        health.TreeHealthChanged.AddListener(HealthChanged);
    }

    private void OnDisable()
    {
        health.Died.RemoveListener(TreeDied);
        attack.TargetDestroyed.RemoveListener(GetNextTarget);
        health.TreeHealthChanged.RemoveListener(HealthChanged);
    }

    void Update()
    {
        CheckHasValidTargetOrFindNewAndAttack();
    }

    private void HealthChanged()
    {
        tree.SetSize();
    }

    private void CheckHasValidTargetOrFindNewAndAttack()
    {
        if (HasEnemyTarget())
        {
            if (!CurrentEnemyIsInRange())
            {
                isAttacking = false;
                attack.ResetTarget();
                GetNextTarget();
            }
        }
        else
        {
            GetNextTarget();
        }
    }

    private bool HasEnemyTarget()
    {
        return attack.HasActiveTarget;

        //if (attack == null)
        //{
        //    return false;
        //}

        //return true;
    }

    private bool CurrentEnemyIsInRange()
    {
        if (Vector3.Distance(transform.position, attack.TargetPosition) <= attackRange)
        {
            return true;
        }

        return false;
    }

    private void GetNextTarget()
    {
        var newTarget = FindTarget();
        if (newTarget != null)
        {
            AttackTarget(newTarget);
        }
    }

    private void AttackTarget(Health target)
    {
        attack.SetTarget(target);
        isAttacking = true;
    }

    private void TreeDied()
    {
        crossroad.DestroyTree();
    }

    private Health FindTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var closestDistance = Mathf.Infinity;

        Health targetGameObject = null;
        foreach (var enemy in enemies)
        {
            var healthComponent = enemy.GetComponent<Health>();
            if (healthComponent == null)
            {
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= attackRange)
            {
                closestDistance = distance;
                targetGameObject = healthComponent;
            }
        }

        return targetGameObject;
    }
}
