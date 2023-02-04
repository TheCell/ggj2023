using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavigationAgent))]
public class NavigationAgent : MonoBehaviour
{
    private Transform goal;
    private NavMeshAgent agent;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform target)
    {
        goal = target;

    }

    private void StartMoving()
    {
        agent.destination = goal.position;
    }

    private void StopMoving()
    {
        agent.destination = transform.position;
    }
}
