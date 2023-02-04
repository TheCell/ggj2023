using UnityEngine;

[RequireComponent(typeof(PlayAudioLocalSource))]
public class EnemyBehaviour : MonoBehaviour
{
    private NavigationAgent agent;
    private Attack attack;

    private PlayAudioLocalSource playAudio;
    [SerializeField] private SOAudioCollection enemyAudio;

    void Start()
    {
        playAudio = GetComponent<PlayAudioLocalSource>();
        agent = GetComponent<NavigationAgent>();
        if (agent == null)
        {
            Debug.LogError("NavigationAgent not present");
        }

        attack = GetComponent<Attack>();
        if (attack == null)
        {
            Debug.LogError("Attack not present");
        }

        agent.StoppedMoving.AddListener(StoppedMoving);
        attack.TargetDestroyed.AddListener(GetNextTarget);
        GetNextTarget();

        if(enemyAudio != null)
        {
            playAudio.PlayAudioClip(enemyAudio.GetSpawnAudio);
        }

        else
        {
            Debug.LogError("Missing SO with audio information in Enemy Behaviour");
        }
    }

    private void OnDisable()
    {
        agent.StoppedMoving.RemoveListener(StoppedMoving);
        attack.TargetDestroyed.RemoveListener(GetNextTarget);
    }

    void Update()
    {
        
    }

    private void StoppedMoving()
    {
        GetNextTarget();
    }

    private void GetNextTarget()
    {
        var newTarget = FindTarget();
        if (newTarget != null)
        {
            MoveOrAttack(newTarget);
        }
        else
        {
            Debug.Log("No Target Found");
        }
    }

    private void MoveOrAttack(Health target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= agent.AgentRange())
        {
            attack.SetTarget(target);
            playAudio.PlayAudioClip(enemyAudio.GetAttackAudio);
        }
        else
        {
            agent.SetTarget(target.transform);
            playAudio.PlayAudioClip(enemyAudio.GetIdleAudio);
        }
    }

    private Health FindTarget()
    {
        var trees = GameObject.FindGameObjectsWithTag("Tree");
        var closestDistance = Mathf.Infinity;

        Health targetGameObject = null;
        foreach (var tree in trees)
        {
            var healthComponent = tree.GetComponent<Health>();
            if (healthComponent == null)
            {
                continue;
            }

            var distance = Vector3.Distance(transform.position, tree.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetGameObject = healthComponent;
            }
        }

        return targetGameObject;
    }
}
