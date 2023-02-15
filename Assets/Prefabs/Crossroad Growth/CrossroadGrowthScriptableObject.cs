using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossroadGrowthConfig", menuName = "ScriptableObjects/Map/CrossroadGrowthConfigScriptableObject", order = 1)]
public class CrossroadGrowthScriptableObject : ScriptableObject
{
    public List<GameObject> TreePrefab;
    public GameObject RootPrefab;
    public int NewBuildTreshhold;
}
