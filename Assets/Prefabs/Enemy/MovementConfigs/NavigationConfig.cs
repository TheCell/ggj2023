using UnityEngine;

[CreateAssetMenu(fileName = "BaseMovementConfig", menuName = "ScriptableObjects/BaseMovementScriptableObject", order = 1)]
public class NavigationConfig : ScriptableObject
{
    public float Speed;
    public float AngularSpeed;
    public float Acceleration;
    public float StoppingDistance;
    public float ObstacleAvoidanceRadius;
}
