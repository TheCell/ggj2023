using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class LevelGeneratorGrid : MonoBehaviour
{

    public GameObject[] buildingObjects;
    public int buildingDensity; // between 1 and 10
    public GameObject treeObject;
    public GameObject crossroadObject;
    public GameObject monumentObject;
    public int fieldSize;
    public GameObject enemySpawner;
    
    private int maxDensity = 10;
    private int[] roationOptions = { 0, 90, 180, 270 };
    private int pointDistance = 5;
    private GameObject[,] crossRoadGrid;
    private int goalMonument = 2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject start = GameObject.Find("NavMeshSceneGeometry");
        //GameObject ground = GameObject.Find("Ground");
        //ground.gameObject.transform.localScale += new Vector3((fieldSize * pointDistance), 0, (fieldSize * pointDistance));
        crossRoadGrid = new GameObject[fieldSize, fieldSize];
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                Vector3 position = new Vector3(i * pointDistance, 0.079f, j * pointDistance);

                crossRoadGrid[i, j] = Instantiate(crossroadObject, position, Quaternion.identity);

                List<GameObject>  bros = new List<GameObject>();
                if (i>0)
                {
                    bros.Add(crossRoadGrid[i-1, j]);
                }
                if (j > 0)
                {
                    bros.Add(crossRoadGrid[i, j-1]);
                }
                crossRoadGrid[i, j].GetComponent<Crossroad>().connectedCrossroads = bros;
            }
        }
        for (int i = 0; i < fieldSize-1; i++)
        {
            for (int j = 0; j < fieldSize - 1; j++)
            {
                Vector3 positionBuilding = new Vector3(i * pointDistance + (pointDistance / 2), 0, j * pointDistance + (pointDistance / 2));
                int rand = UnityEngine.Random.Range(0, buildingObjects.Length);
                bool building = UnityEngine.Random.Range(0, maxDensity) < buildingDensity;
                int rotation = UnityEngine.Random.Range(0, roationOptions.Length);
                GameObject b;
                if (i== goalMonument && j== goalMonument)
                {
                    b = Instantiate(monumentObject, new Vector3(pointDistance + (pointDistance / 2), 0, pointDistance + (pointDistance / 2)), Quaternion.identity);
                    connectAdjacentCrossroadsToBuilding(b, i, j);

                } else if(building)
                {
                    b= Instantiate(buildingObjects[rand], positionBuilding, Quaternion.Euler(new Vector3(0, roationOptions[rotation], 0)));
                    connectAdjacentCrossroadsToBuilding(b, i, j);
                }
            }
        }

        //Base
        int center = (fieldSize / 2) -1;
        crossRoadGrid[center, center].GetComponent<Crossroad>().startsWithTree = true;
        crossRoadGrid[center + 1, center].GetComponent<Crossroad>().startsWithTree = true;
        crossRoadGrid[center, center + 1].GetComponent<Crossroad>().startsWithTree = true;
        crossRoadGrid[center + 1, center + 1].GetComponent<Crossroad>().startsWithTree = true;


        // Initiate spawner
        Instantiate(enemySpawner, new Vector3(-5, 0,-5), Quaternion.identity);


        Debug.Log("Grid setup finished");

    }

    // Update is called once per frame
    void Update()
    {

    }

   void connectAdjacentCrossroadsToBuilding(GameObject b, int i, int j)
    {
        connectAdjacentCrossroad(b, i, j);
        connectAdjacentCrossroad(b, i + 1, j);
        connectAdjacentCrossroad(b, i, j + 1);
        connectAdjacentCrossroad(b, i + 1, j + 1);

}
    void connectAdjacentCrossroad(GameObject b, int i, int j)
    {
        if (crossRoadGrid[i, j].GetComponent<Crossroad>().adjacentBuildings == null) { crossRoadGrid[1, 1].GetComponent<Crossroad>().adjacentBuildings = new List<GameObject>(); }
        crossRoadGrid[i, j].GetComponent<Crossroad>().adjacentBuildings.Add(b);
    }
}
