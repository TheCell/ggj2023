using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadConstants : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject rootPrefab;
    public int crossroadNewBuildTreshhold;

    public void RedrawAllRoots()
    {
        foreach (GameObject crossroad in GameObject.FindGameObjectsWithTag("Crossroad"))
        {
            crossroad.GetComponent<Crossroad>().RedrawRoots();
        }
    }
}
