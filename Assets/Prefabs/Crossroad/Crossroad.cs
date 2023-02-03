using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedCrossroads = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject crossroad in connectedCrossroads)
        {
            crossroad.GetComponent<Crossroad>().EnsureConnectionBothWays(this.gameObject);
        }
    }

    public void EnsureConnectionBothWays(GameObject otherCrossroad)
    {
        if(!connectedCrossroads.Contains(otherCrossroad))
        {
            connectedCrossroads.Add(otherCrossroad);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
