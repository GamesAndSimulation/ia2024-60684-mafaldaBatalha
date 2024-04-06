using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public DiscoSystem discoSystem;
    public GameObject enemy;
    public float speed, totalLives;

  
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
            currentLives--;

            if (currentLives == 0)
            {
                discoSystem.KillAnEnemy();
                enemy.gameObject.SetActive(false);
            }
        }
            
    }


}
