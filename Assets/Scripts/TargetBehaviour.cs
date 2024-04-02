using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public GameObject projectile;
    public float fireforce;
    public float speedRange;
    public float directionTime;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeDirection", 0, Random.Range(directionTime/1.5f, directionTime));
    }

    void ChangeDirection()
    {
        direction = new Vector3(Random.Range(-speedRange, speedRange), 0, Random.Range(-speedRange, speedRange));
        FireProjectile();
    }

    void FireProjectile()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var fireDirection = player.transform.position - transform.position;
        fireDirection.Normalize();
        GameObject instantiatedBullet =
            Instantiate(projectile, transform.position + fireDirection, transform.rotation);
        instantiatedBullet.GetComponent<Rigidbody>().AddForce(fireDirection * fireforce);
        Destroy(instantiatedBullet, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction;
    }
}
