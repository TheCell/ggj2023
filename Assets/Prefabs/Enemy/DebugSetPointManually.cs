using UnityEngine;

[RequireComponent(typeof(NavigationAgent))]
public class DebugSetPoint : MonoBehaviour
{
    [SerializeField] private Transform debugTarget;
    private NavigationAgent navigationAgent;
    private float lastUpdateTimestamp = 0f;

    void OnEnable()
    {
        navigationAgent = GetComponent<NavigationAgent>();
    }

    private void Start()
    {
        navigationAgent.SetTarget(debugTarget);
    }

    private void Update()
    {
        if (Time.time + 1f > lastUpdateTimestamp)
        {
            lastUpdateTimestamp = Time.time;
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        navigationAgent.SetTarget(debugTarget);
    }
}
