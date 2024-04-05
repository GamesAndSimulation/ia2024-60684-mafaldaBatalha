using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoSystem : MonoBehaviour
{
    public GameObject discoBall;
    public GameObject disco;
    public Collider boxCollider;
    public float speed;
    public float enemyNum;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the disco ball rotate
        discoBall.transform.eulerAngles = new Vector3(0f, speed * Time.time, 0f);
    }

    public void killAnEnemy()
    {
        enemyNum--;

        if(enemyNum <= 0)
        {
            disco.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            disco.SetActive(true);
            boxCollider.enabled = false;

        }
    }
}
