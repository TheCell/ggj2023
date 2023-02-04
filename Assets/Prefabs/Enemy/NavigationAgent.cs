using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavigationAgent))]
public class NavigationAgent : MonoBehaviour
{
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

    public void SetTarget(Transform target)
    {
        goal = target;
        StartMoving();
    }

    private void StartMoving()
    {
        agent.destination = goal.position;
    }

    private void StopMovingImmediately()
    {
        agent.destination = transform.position;
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
