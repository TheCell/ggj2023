using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] objects;
    public int buildingDensity; // between 1 and 10
    private int maxDensity = 10;
    private int[] roationOptions = { 0, 90, 180, 270 };

    // Start is called before the first frame update
    void Start()
    {
        int rand = UnityEngine.Random.Range(0, objects.Length);
        bool building = UnityEngine.Random.Range(0, maxDensity) < buildingDensity;
        int rotation = UnityEngine.Random.Range(0, roationOptions.Length);
        if (building)
        {
            Instantiate(objects[rand], transform.position, Quaternion.Euler(new Vector3(0, roationOptions[rotation], 0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
