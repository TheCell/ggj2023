using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrowth : MonoBehaviour
{
    [SerializeField] private List<GameObject> surroundingCrossroads = new List<GameObject>();
    public bool isMonument = false;

    private GameObject building;
    private Overgrowth overgrowth;

    private void OnEnable()
    {
        building = gameObject;
        building.SetActive(true);
        overgrowth = building.GetComponentInChildren<Overgrowth>();
        Console.WriteLine("Building overgrowth?");
        Console.WriteLine(overgrowth);
        overgrowth.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        foreach (GameObject crossroad in surroundingCrossroads)
        {
            crossroad.GetComponent<CrossroadGrowth>().EnsureBuildingConnectionBothWays(this.gameObject);
        }
        RedrawBuilding();
    }

    public void EnsureBuildingConnectionBothWays(GameObject otherGameobject)
    {
        if (!surroundingCrossroads.Contains(otherGameobject))
        {
            surroundingCrossroads.Add(otherGameobject);
        }
    }

    public void RedrawBuilding()
    {
        if (ShouldBeOvergrown() != IsOvergrown())
        {
            if (ShouldBeOvergrown())
            {
                overgrowth.transform.localScale = Vector3.one;
                //Audio
            }
            else
            {
                overgrowth.transform.localScale = Vector3.zero;
                //Audio
            }
        }
    }

    public bool ShouldBeOvergrown()
    {
        foreach (GameObject crossroad in surroundingCrossroads)
        {
            if(!crossroad.GetComponent<CrossroadGrowth>().HasTree())
            {
                return false;
            }
        }
        return true;
    }

    public bool IsOvergrown()
    {
        return overgrowth.transform.localScale == Vector3.one;
    }
}
