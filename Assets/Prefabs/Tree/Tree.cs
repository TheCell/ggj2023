using UnityEngine;

public class Tree : MonoBehaviour
{
    public void SetSize()
    {
        float scalingFactor = 1f * transform.GetComponent<Health>().getCurrentHealth() / transform.GetComponent<Health>().getStartingHealth();
        transform.localScale = new Vector3(scalingFactor, scalingFactor, scalingFactor);
    }
}
