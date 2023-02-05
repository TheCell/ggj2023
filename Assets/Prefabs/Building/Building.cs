using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private List<GameObject> surroundingCrossroads = new List<GameObject>();
    [SerializeField] private List<GameObject> buildingChoices = new();

    private GameObject building;
    private Overgrowth overgrowth;

    private void OnEnable()
    {
        var random = new System.Random();
        var randomChoiceIndex = random.Next(buildingChoices.Count);
        foreach (var choice in buildingChoices)
        {
            choice.SetActive(false);
        }

        building = buildingChoices[randomChoiceIndex];
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
        if (CheckOvergrowthStatus())
        {
            overgrowth.transform.localScale = Vector3.one;
            if (transform.GetComponent<Monument>())
            {
                transform.GetComponent<Monument>().WinGame();
            }
        }
        else
        {
            overgrowth.transform.localScale = Vector3.zero;
        }
    }

    private bool CheckOvergrowthStatus()
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
}
