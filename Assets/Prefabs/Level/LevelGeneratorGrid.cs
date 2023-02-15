using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorGrid : MonoBehaviour
{
    public GameObject[] buildingObjects;
    public int buildingDensity; // between 1 and 100 (maxDensity)
    public GameObject crossroadObject;
    public GameObject monumentObject;
    public int fieldSize;
    public GameObject enemySpawner;
    public int enemySpawnerPoints; // between 1 and 4

    private int maxDensity = 100;
    private int[] roationOptions = { 0, 90, 180, 270 };
    private GameObject[,] crossRoadGrid;
    private List<GameObject> buildings = new List<GameObject>();
    private List<GameObject> enemySpawners = new List<GameObject>();

    // Positioning Variables
    private int startGrid = 20;
    private int monumentPosition = 1;
    private int pointDistance = 5;
    private int EnemySpawnerDistanceToGridCorner = 4;
    private float currentBuildingAssetNullpointCorrection;
    private int enemyStartPoint;
    private int enemyStartPointCorner;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    private void Setup()
    {
        SetupBaseVariables();
        //GameObject start = GameObject.Find("NavMeshSceneGeometry");
        //GameObject ground = GameObject.Find("Ground");
        //ground.gameObject.transform.localScale += new Vector3((fieldSize * pointDistance), 0, (fieldSize * pointDistance));
        BuildCrossroadGrid();
        AddBuildingsAndMonument();
        PlantTreesAtCenter();
        InstantiateEnemySources();
    }
    private void SetupBaseVariables()
    {
        currentBuildingAssetNullpointCorrection = pointDistance / 2 + 0.6f;
        enemyStartPoint = startGrid - EnemySpawnerDistanceToGridCorner;
        enemyStartPointCorner = startGrid + fieldSize * pointDistance + EnemySpawnerDistanceToGridCorner;
        crossRoadGrid = new GameObject[fieldSize, fieldSize];
        buildings = new List<GameObject>();
        enemySpawners = new List<GameObject>();
}

    private void BuildCrossroadGrid()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                Vector3 position = new Vector3(startGrid + i * pointDistance, 0.079f, startGrid + j * pointDistance);

                crossRoadGrid[i, j] = Instantiate(crossroadObject, position, Quaternion.identity);

                List<GameObject> bros = new List<GameObject>();
                if (i > 0)
                {
                    bros.Add(crossRoadGrid[i - 1, j]);
                }
                if (j > 0)
                {
                    bros.Add(crossRoadGrid[i, j - 1]);
                }
                crossRoadGrid[i, j].GetComponent<CrossroadGrowth>().connectedCrossroads = bros;
            }
        }
    }
    private void PlantTreesAtCenter()
    {
        int center = (fieldSize / 2) - 1;
        crossRoadGrid[center, center].GetComponent<CrossroadGrowth>().startsWithTree = true;
        crossRoadGrid[center + 1, center].GetComponent<CrossroadGrowth>().startsWithTree = true;
        crossRoadGrid[center, center + 1].GetComponent<CrossroadGrowth>().startsWithTree = true;
        crossRoadGrid[center + 1, center + 1].GetComponent<CrossroadGrowth>().startsWithTree = true;
    }
    private void AddBuildingsAndMonument()
    {
        for (int i = 0; i < fieldSize - 1; i++)
        {
            for (int j = 0; j < fieldSize - 1; j++)
            {
                int rand = UnityEngine.Random.Range(0, buildingObjects.Length);
                bool build = UnityEngine.Random.Range(0, maxDensity) < buildingDensity;

                if (i == monumentPosition && j == monumentPosition)
                {
                    GameObject b = AddBuilding(monumentObject, i, j);
                    BuildingGrowth script = (BuildingGrowth)b.GetComponent(typeof(BuildingGrowth));
                    script.isMonument = true;
                }
                else if (build)
                {
                    GameObject b = AddBuilding(buildingObjects[rand], i, j);
                }
            }
        }
    }
    private GameObject AddBuilding(GameObject building, int i, int j)
    {
        Vector3 positionBuilding = new Vector3(startGrid + i * pointDistance + currentBuildingAssetNullpointCorrection, 0, startGrid + j * pointDistance + currentBuildingAssetNullpointCorrection);
        int rotation = UnityEngine.Random.Range(0, roationOptions.Length);
        GameObject b = Instantiate(building, positionBuilding, Quaternion.Euler(new Vector3(0, roationOptions[rotation], 0)));
        ConnectAdjacentCrossroadsToBuilding(b, i, j);
        buildings.Add(b);
        return b;
    }
    private void ConnectAdjacentCrossroadsToBuilding(GameObject b, int i, int j)
    {
        AddBuildingToListInCrossroad(b, i, j);
        AddBuildingToListInCrossroad(b, i + 1, j);
        AddBuildingToListInCrossroad(b, i, j + 1);
        AddBuildingToListInCrossroad(b, i + 1, j + 1);
    }
    private void AddBuildingToListInCrossroad(GameObject b, int i, int j)
    {
        if (crossRoadGrid[i, j].GetComponent<CrossroadGrowth>().adjacentBuildings == null)
        {
            crossRoadGrid[1, 1].GetComponent<CrossroadGrowth>().adjacentBuildings = new List<GameObject>();
        }
        crossRoadGrid[i, j].GetComponent<CrossroadGrowth>().adjacentBuildings.Add(b);
    }

    private void InstantiateEnemySources()
    {
        GameObject spawner = Instantiate(enemySpawner, new Vector3(enemyStartPoint, 0, enemyStartPoint), Quaternion.identity);
        enemySpawners.Add(spawner);
        if (enemySpawnerPoints > 1)
        {
            spawner = Instantiate(enemySpawner, new Vector3(enemyStartPointCorner, 0, enemyStartPointCorner), Quaternion.identity);
            enemySpawners.Add(spawner);
            if (enemySpawnerPoints > 2)
            {
                spawner = Instantiate(enemySpawner, new Vector3(enemyStartPoint, 0, enemyStartPointCorner), Quaternion.identity);
                enemySpawners.Add(spawner);
                if (enemySpawnerPoints > 3)
                {
                    spawner = Instantiate(enemySpawner, new Vector3(enemyStartPointCorner, 0, enemyStartPoint), Quaternion.identity);
                    enemySpawners.Add(spawner);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (crossRoadGrid[0,0] == null)
        {
            Setup();
        }
        foreach (GameObject b in buildings)
        {
            BuildingGrowth script = (BuildingGrowth)b.GetComponent(typeof(BuildingGrowth));
            script.RedrawBuilding();
            if (script.IsOvergrown() && script.isMonument)
            {
                WinGame();
            }
        }
        if (GameObject.FindGameObjectsWithTag("Tree").Length == 0)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("You won!");
        CleanupBoard();
        GameSceneSwitcher sceneSwitcher = gameObject.AddComponent<GameSceneSwitcher>();
        sceneSwitcher.SwitchToWinScene();
    }
    private void LoseGame()
    {
        Debug.Log("You lost!");
        CleanupBoard();
        GameSceneSwitcher sceneSwitcher = gameObject.AddComponent<GameSceneSwitcher>();
        sceneSwitcher.SwitchToLooseScene();
    }
    private void CleanupBoard()
    {
        foreach (GameObject obj in crossRoadGrid) {
            Destroy(obj);
        }
        foreach (GameObject obj in buildings)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in enemySpawners)
        {
            Destroy(obj);
        }
        SetupBaseVariables();
    }
}
