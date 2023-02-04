using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private List<GameObject> surroundingCrossroads = new List<GameObject>();

    private Renderer buildingRenderer;

    private void Awake()
    {
        buildingRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
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
            buildingRenderer.material.color = Color.green;
            if(transform.GetComponent<Monument>())
            {
                transform.GetComponent<Monument>().WinGame();
            }
        }
        else
        {
            buildingRenderer.material.color = Color.grey;
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
