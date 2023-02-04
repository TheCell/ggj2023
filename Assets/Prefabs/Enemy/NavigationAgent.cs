using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavigationAgent))]
public class NavigationAgent : MonoBehaviour
{
    public UnityEvent StartedMoving = new();
    public UnityEvent StoppedMoving = new();

    [SerializeField] private NavigationConfig config;
    private Transform goal;
    private NavMeshAgent agent;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        if (config != null)
        {
            ApplyNavigationConfig(config);
        }
    }

    private void Update()
    {
        CheckStoppingConditions();
    }

    public float AgentRange()
    {
        return agent.stoppingDistance;
    }

    public void SetTarget(Transform target)
    {
        goal = target;
        StartMoving();
    }

    private void CheckStoppingConditions()
    {
        if (agent.isStopped)
        {
            return;
        }

        float remainingDist = agent.remainingDistance;
        if (remainingDist <= agent.stoppingDistance)
        {
            StopMoving();
            StoppedMoving.Invoke();
        }
    }

    private void StartMoving()
    {
        agent.destination = goal.position;
        agent.isStopped = false;
        StartedMoving.Invoke();
    }

    private void StopMoving()
    {
        agent.isStopped = true;
    }

    private void ApplyNavigationConfig(NavigationConfig config)
    {
        agent.speed = config.Speed;
        agent.angularSpeed = config.AngularSpeed;
        agent.acceleration = config.Acceleration;
        agent.stoppingDistance = config.StoppingDistance;
        agent.radius = config.ObstacleAvoidanceRadius;
    }
}
