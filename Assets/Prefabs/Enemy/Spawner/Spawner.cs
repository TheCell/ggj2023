using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnerScriptableObject spawnerScriptableObject;

    private List<GameObject> spawnableEnemies;
    private float spawnRate;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        ApplyConfig(spawnerScriptableObject);
        spawnRate *= 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawnTime + spawnRate)
        {
            lastSpawnTime = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = GetRandomEnemy();
        enemyPrefab = Instantiate(enemyPrefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
        enemyPrefab.transform.parent = this.transform;
    }

    private GameObject GetRandomEnemy()
    {
        return spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
    }

    public void ApplyConfig(SpawnerScriptableObject spawnerScriptableObject)
    {
        spawnableEnemies = spawnerScriptableObject.SpawnableEnemies;
        spawnRate = spawnerScriptableObject.SpawnRate;
    }
}
