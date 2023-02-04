using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfig", menuName = "ScriptableObjects/HP/AttackScriptableObject", order = 2)]
public class AttackConfig : ScriptableObject
{
    public int AttackDamage;
    public float TimeBetweenAttacks;
}
