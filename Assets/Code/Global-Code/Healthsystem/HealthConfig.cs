using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "ScriptableObjects/HP/HealthConfigScriptableObject", order = 1)]
public class HealthScriptableObject : ScriptableObject
{
    public int MaxHealth;
    public int StartingHealth;
}
