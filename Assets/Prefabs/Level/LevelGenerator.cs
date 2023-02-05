using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] buildingObjects; 
    public int buildingDensity; // between 1 and 10
    public GameObject treeObject;
   public GameObject crossroadObject;
    public int fieldSize;

    private int maxDensity = 10;
    private int[] roationOptions = { 0, 90, 180, 270 };
    private int pointDistance = 5;
    private GameObject[,] crossRoadGrid;

    // Start is called before the first frame update
    void Start()
    {
        GameObject start = GameObject.Find("GridStart");
        crossRoadGrid = new GameObject[fieldSize,fieldSize];
        for ( int i = 0; i>fieldSize; i++) {
            for(int j = 0; j > fieldSize; j++) {



                /*int rand = UnityEngine.Random.Range(0, buildingObjects.Length);
                bool building = UnityEngine.Random.Range(0, maxDensity) < buildingDensity;
                int rotation = UnityEngine.Random.Range(0, roationOptions.Length);
                if (building)
                {
                    crossRoadGrid[i, j] = Instantiate(buildingObjects[rand], transform.position, Quaternion.Euler(new Vector3(0, roationOptions[rotation], 0)));
                }*/
            }
        }


    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
