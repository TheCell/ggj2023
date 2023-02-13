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
        overgrowth = building.GetComponentInChildren<Overgrowth>();
        building.SetActive(true);
        overgrowth.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        foreach (GameObject crossroad in surroundingCrossroads)
        {
            crossroad.GetComponent<Crossroad>().EnsureBuildingConnectionBothWays(this.gameObject);
        }
        RedrawBuilding();
    }

    private void EnsureBuildingConnectionBothWays(GameObject otherGameobject)
    {
        if (!surroundingCrossroads.Contains(otherGameobject))
        {
            surroundingCrossroads.Add(otherGameobject);
        }
    }

    public void RedrawBuilding()
    {
        if (shouldBeOvergrown() != isOvergrown())
        {
            if (shouldBeOvergrown())
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

    public bool shouldBeOvergrown()
    {
        foreach (GameObject crossroad in surroundingCrossroads)
        {
            if(!crossroad.GetComponent<Crossroad>().HasTree())
            {
                return false;
            }
        }
        return true;
    }

    public bool isOvergrown()
    {
        return overgrowth.transform.localScale == Vector3.one;
    }
}
