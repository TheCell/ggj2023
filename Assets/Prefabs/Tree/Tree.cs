using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int startingHealth;

    private int currentHealth;
    private Renderer treeRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        treeRenderer = GetComponent<Renderer>();
        // remove this once we have actual models & textures
        treeRenderer.material.color = Color.green;

        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Heal(int damage)
    {
        currentHealth += damage;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
