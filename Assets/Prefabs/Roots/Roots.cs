using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roots : MonoBehaviour
{
    public void DrawRoot(Transform startPoint, Transform endPoint)
    {
        this.transform.position = new Vector3(startPoint.position.x + endPoint.position.x, startPoint.position.y + endPoint.position.y, startPoint.position.z + endPoint.position.z) / 2f;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, endPoint.position - startPoint.position);
        this.transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
