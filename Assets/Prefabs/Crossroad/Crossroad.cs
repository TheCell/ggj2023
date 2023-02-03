using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedCrossroads = new List<GameObject>();

    private GameObject treeGameObject;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject crossroad in connectedCrossroads)
        {
            crossroad.GetComponent<Crossroad>().EnsureConnectionBothWays(this.gameObject);
        }

        // Debug
        PlantTree();
    }

    public void EnsureConnectionBothWays(GameObject otherCrossroad)
    {
        if(!connectedCrossroads.Contains(otherCrossroad))
        {
            connectedCrossroads.Add(otherCrossroad);
        }
    }

    private void PlantTree()
    {
        GameObject treePrefab = transform.parent.gameObject.GetComponent<CrossroadConstants>().treePrefab;
        treeGameObject = Instantiate(treePrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        treeGameObject.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
