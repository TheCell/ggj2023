using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossroadConfig", menuName = "ScriptableObjects/Map/CrossroadConfigScriptableObject", order = 1)]
public class CrossroadScriptableObject : ScriptableObject
{
    public List<GameObject> TreePrefab;
    public GameObject RootPrefab;
    public int NewBuildTreshhold;
}
