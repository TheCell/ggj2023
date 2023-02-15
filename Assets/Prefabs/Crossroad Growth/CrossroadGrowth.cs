using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CrossroadGrowth : MonoBehaviour
{
    [SerializeField] private CrossroadGrowthScriptableObject crossroadScriptableObject;
    [SerializeField] public List<GameObject> connectedCrossroads = new List<GameObject>();
    [SerializeField] public List<GameObject> adjacentBuildings = new List<GameObject>();
    [SerializeField] public bool startsWithTree;

    private GameObject treeGameObject;
    private List<GameObject> rootGameObjects = new List<GameObject>();
    private int treePrepStatus = 0;

    private int newBuildTreshhold;
    private List<GameObject> treePrefab;
    private GameObject rootPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Inside crossroad");
        ApplyConfig(crossroadScriptableObject);

        foreach (GameObject crossroad in connectedCrossroads)
        {
            crossroad.GetComponent<CrossroadGrowth>().EnsureStreetConnectionBothWays(this.gameObject);
        }
        foreach (GameObject building in adjacentBuildings)
        {
            building.GetComponent<BuildingGrowth>().EnsureBuildingConnectionBothWays(this.gameObject);
        }

        if (startsWithTree)
        {
            Console.WriteLine("We plant a tree!");
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

    public void PrepareTree()
    {
        if (CheckIfTreePreparable())
        {
            treePrepStatus++;
            if (treePrepStatus >= newBuildTreshhold)
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
            if (crossroad.GetComponent<CrossroadGrowth>().HasTree())
            {
                return true;
            }
        }

        return false;
    }

    private void PlantTree()
    {
        treeGameObject = Instantiate(treePrefab[0], transform.GetChild(0).position, transform.GetChild(0).rotation);
        treeGameObject.transform.parent = this.transform;
        RedrawEverything();
    }

    public void DestroyTree()
    {
        float startTime = Time.time;
        Destroy(treeGameObject);
        treeGameObject = null;
        RedrawEverything();
    }


    private void RedrawEverything()
    {
        RedrawAllAdjacentBuildings();
        RedrawAllRoots();
    }

    private void RedrawAllAdjacentBuildings()
    {
        foreach (GameObject building in adjacentBuildings)
        {
            building.GetComponent<BuildingGrowth>().RedrawBuilding();
        }
    }

    private void RedrawAllRoots()
    {
        foreach (GameObject crossroad in GameObject.FindGameObjectsWithTag("Crossroad"))
        {
            crossroad.GetComponent<CrossroadGrowth>().RedrawRoots();
        }
    }

    public bool HasTree()
    {
        return treeGameObject != null;
    }

    public int ConnectedTreesAmount()
    {
        int amount = 0;

        foreach (GameObject crossroad in connectedCrossroads)
        {
            if (crossroad.GetComponent<CrossroadGrowth>().HasTree())
            {
                amount++;
            }
        }

        return amount;
    }

    public void RedrawRoots()
    {
        foreach (GameObject root in rootGameObjects)
        {
            Destroy(root);
        }
        rootGameObjects = new List<GameObject>();

        if (HasTree())
        {
            foreach (GameObject crossroad in connectedCrossroads)
            {
                if (crossroad.GetComponent<CrossroadGrowth>().HasTree())
                {
                    GameObject rootGameObject = Instantiate(rootPrefab, new Vector3(0, 0, 0), new Quaternion());
                    rootGameObject.GetComponent<Roots>().DrawRoot(transform.GetChild(0), crossroad.transform.GetChild(0));
                    rootGameObject.transform.parent = this.transform;
                    rootGameObjects.Add(rootGameObject);
                }
            }
        }
    }
    public void ApplyConfig(CrossroadGrowthScriptableObject crossroadConfig)
    {
        this.newBuildTreshhold = crossroadConfig.NewBuildTreshhold;
        this.treePrefab = crossroadConfig.TreePrefab;
        this.rootPrefab = crossroadConfig.RootPrefab;
    }

    public void ContextMenuAction(ActionUiType type)
    {
        if (CheckIfTreePreparable())
        {
            treePrepStatus++;
            if (treePrepStatus >= newBuildTreshhold)
            {
                ReplaceTree(type);
                treePrepStatus = 0;
            }
        }
    }

    private void ReplaceTree(ActionUiType type)
    { 
        GameObject newTree;
        GameObject prefab;
        float healthRatio = 1;

        if (treeGameObject)
        {
            int formerCurrentHealth = treeGameObject.GetComponent<Health>().getCurrentHealth();
            int formerStartHealth = treeGameObject.GetComponent<Health>().getStartingHealth();
            healthRatio = 1f * formerCurrentHealth / formerStartHealth;
        }

        switch (type)
        {
            case ActionUiType.Top:
                Debug.Log("top");
                prefab = treePrefab[1];
                break;
            case ActionUiType.Right:
                Debug.Log("right");
                prefab = treePrefab[2];
                break;
            case ActionUiType.Left:
                Debug.Log("left"); 
                prefab = treePrefab[3];
                break;
            case ActionUiType.Bottom:
            default:
                Debug.Log("bottom");
                prefab = treePrefab[0];
                break;
        }

        newTree = Instantiate(prefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
        newTree.transform.parent = this.transform;
        StartCoroutine(SetTreeHealthAfterReplacement(healthRatio));

        DestroyTree();
        treeGameObject = newTree;

        RedrawEverything();
    }

    private IEnumerator SetTreeHealthAfterReplacement(float ratio)
    {
        yield return new WaitForEndOfFrame();
        treeGameObject.GetComponent<Health>().SetHealthByRatio(ratio);
    }
}
