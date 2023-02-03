using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private int health;

    private Renderer treeRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        treeRenderer = GetComponent<Renderer>();
        // remove this once we have actual models & textures
        treeRenderer.material.color = Color.green;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
