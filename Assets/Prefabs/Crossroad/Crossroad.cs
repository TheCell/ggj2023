using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedCrossroads = new List<GameObject>();
    [SerializeField] private List<GameObject> adjacentBuildings = new List<GameObject>();
    [SerializeField] private bool startsWithTree;

    private GameObject treeGameObject;
    private List<GameObject> rootGameObjects = new List<GameObject>();
    private int treePrepStatus = 0;
    private int newBuildTreshhold;


    // Start is called before the first frame update
    void Start()
    {
        newBuildTreshhold = transform.parent.gameObject.GetComponent<CrossroadConstants>().crossroadNewBuildTreshhold;

        foreach (GameObject crossroad in connectedCrossroads)
        {
            crossroad.GetComponent<Crossroad>().EnsureStreetConnectionBothWays(this.gameObject);
        }
        foreach (GameObject building in adjacentBuildings)
        {
            building.GetComponent<Building>().EnsureBuildingConnectionBothWays(this.gameObject);
        }

        if (startsWithTree)
        {
            PlantTree();
        }
    }

    public void EnsureStreetConnectionBothWays(GameObject otherGameobject)
    {
        if (!connectedCrossroads.Contains(otherGameobject))
        {
            connectedCrossroads.Add(otherGameobject);
        }
    }
    public void EnsureBuildingConnectionBothWays(GameObject otherGameobject)
    {
        if (!adjacentBuildings.Contains(otherGameobject))
        {
            adjacentBuildings.Add(otherGameobject);
        }
    }

    private void PrepareTree()
    {
        if (!HasTree() && CheckIfTreePreparable())
        {
            treePrepStatus++;
            if(treePrepStatus >= newBuildTreshhold) 
            {
                PlantTree();
                treePrepStatus = 0;
            }
        }
    }

    private bool CheckIfTreePreparable()
    {
        foreach (GameObject crossroad in connectedCrossroads)
        {
            if (crossroad.GetComponent<Crossroad>().HasTree())
            {
                return true;
            }
        }

        return false;
    }

    private void PlantTree()
    {
        GameObject treePrefab = transform.parent.gameObject.GetComponent<CrossroadConstants>().treePrefab;
        treeGameObject = Instantiate(treePrefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
        treeGameObject.transform.parent = this.transform;

        RedrawEverything();
    }

    public void DestroyTree()
    {
        float startTime = Time.time;
        Destroy(treeGameObject);
        treeGameObject = null;
        RedrawEverything();
        StartCoroutine(CheckIfGameLost());
    }

    private IEnumerator CheckIfGameLost()
    {
        yield return new WaitForEndOfFrame();
        if(GameObject.FindGameObjectsWithTag("Tree").Length == 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        Debug.Log("You lost!");
        GameSceneSwitcher sceneSwitcher = new GameSceneSwitcher();
        sceneSwitcher.SwitchToStartScene();
    }

    private void RedrawEverything()
    {
        RedrawAllAdjacentBuildings();
        transform.parent.GetComponent<CrossroadConstants>().RedrawAllRoots();
    }

    private void RedrawAllAdjacentBuildings()
    {
        foreach (GameObject building in adjacentBuildings)
        {
            building.GetComponent<Building>().RedrawBuilding();
        }
    }

    public bool HasTree() 
    {
        return treeGameObject != null;
    }

    public void RedrawRoots()
    {
        foreach(GameObject root in rootGameObjects)
        {
            Destroy(root);
        }
        rootGameObjects = new List<GameObject>();

        if (HasTree())
        {
            foreach (GameObject crossroad in connectedCrossroads)
            {
                if (crossroad.GetComponent<Crossroad>().HasTree())
                {
                    GameObject rootPrefab = transform.parent.gameObject.GetComponent<CrossroadConstants>().rootPrefab;
                    GameObject rootGameObject = Instantiate(rootPrefab, new Vector3(0, 0, 0), new Quaternion());
                    rootGameObject.GetComponent<Roots>().DrawRoot(transform.GetChild(0), crossroad.transform.GetChild(0));
                    rootGameObject.transform.parent = this.transform;
                    rootGameObjects.Add(rootGameObject);
                }
            }
        }
    }
}
