using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float maxHealth = 4;
    public float currentHealth;
    public float defaultDamage = 3;

    public AudioClip clip;
    // Start is called before the first frame update

    public GameObject explosion;

    void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }

            GameObject exp = Instantiate(explosion, collision.transform);

            exp.GetComponent<SphereBehaviour>().explode();
            Destroy(exp, 1);

            //we could destroy the hazardous bullet, or use a damage to reduce heath
            //Destroy(collision.gameObject);
            //currentHealth -= collision.gameObject.GetComponent<BulletBehaviour>().Damage;
            currentHealth -= defaultDamage;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
