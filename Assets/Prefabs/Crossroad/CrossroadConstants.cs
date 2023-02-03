using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadConstants : MonoBehaviour
{
    public GameObject treePrefab;
    public int treeTickDamageRate;
    public int treeTickDamageAmount;

    private int fixedUpdateCounter = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fixedUpdateCounter >= treeTickDamageRate)
        {
            foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
            {
                tree.GetComponent<Tree>().TakeDamage(treeTickDamageAmount);
            }
            
            fixedUpdateCounter = 0;
        }
        else
        {
            fixedUpdateCounter++;
        }
    }
}
