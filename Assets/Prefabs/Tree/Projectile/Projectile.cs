using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        StartCoroutine(DestroyAfter(5));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // TODO: call take damage on enemy
            Instantiate(explosion, this.transform.position, this.transform.rotation);

            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfter(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }

}
