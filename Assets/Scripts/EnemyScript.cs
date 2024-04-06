using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public DiscoSystem discoSystem;
    public GameObject enemy;
    public float speed, totalLives;
    public Animator animator;
  
    private float currentLives;

    //private float t; //alternative version

    // Start is called before the first frame update
    void Start()
    {
        currentLives = totalLives;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("a");

            currentLives--;
            animator.SetBool("isDizzy", true);


            if (currentLives == 0)
            {
                StartCoroutine(waiter2(0.5f));
                animator.SetBool("isDead", true);
            }

           StartCoroutine(waiter(0.25f));
        }
            
    }
    IEnumerator waiter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("isDizzy", false);
    }

    IEnumerator waiter2(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        discoSystem.KillAnEnemy();
        enemy.gameObject.SetActive(false);
    }

}
