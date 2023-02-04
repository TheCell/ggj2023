using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public GameObject projectile;

    private LayerMask enemyMask;
    private int projectileSpeed = 5;
    private bool canShoot = true;
    private int cooldown = 3;
    private int range = 10;

    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
    }


    public void Update()
    {
        if (!canShoot) return;

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, enemyMask);
        foreach (var hitCollider in hitColliders)
        {
            Shoot((hitCollider.GetComponent<Transform>().position - this.transform.position).normalized);
        }
    }

    private void Shoot(Vector3 direction)
    {
        canShoot = false;
        StartCoroutine(ResetCanShoot());
        GameObject instProjectile = Instantiate(projectile, this.transform);
        instProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectileSpeed, 0, direction.z * projectileSpeed);
    }

    private IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
