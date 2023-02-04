using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int startingHealth;
    [SerializeField] private int attack;

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

        SetSize();
    }

    public void Heal(int damage)
    {
        currentHealth += damage;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        SetSize();
    }

    private void SetSize()
    {
        float scalingFactor = 1f * currentHealth / startingHealth;
        transform.localScale = new Vector3(scalingFactor, scalingFactor, scalingFactor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            //other.GetComponent<Enemy>().TakeDamage(attack);
        }
    }
}
