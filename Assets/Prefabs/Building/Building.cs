using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private List<GameObject> surroundingCrossroads = new List<GameObject>();

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
}
