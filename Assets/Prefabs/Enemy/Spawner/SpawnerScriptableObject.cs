using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerConfig", menuName = "ScriptableObjects/EnemySpawn/SpawnerConfigScriptableObject", order = 1)]
public class SpawnerScriptableObject : ScriptableObject
{
    public List<GameObject> SpawnableEnemies;
    public float SpawnRate;
}
