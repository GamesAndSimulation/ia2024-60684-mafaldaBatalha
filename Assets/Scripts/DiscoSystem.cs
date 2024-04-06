using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoSystem : MonoBehaviour
{
    public GameObject discoBall, disco, enemies;
    public Collider boxCollider;
    public PaperPlane paperPlane;
    public float discoBallSpeed, enemyNum;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the disco ball rotate
        discoBall.transform.eulerAngles = new Vector3(0f, discoBallSpeed * Time.time, 0f);
    }

    public void KillAnEnemy()
    {
        enemyNum--;

        if(enemyNum <= 0)
        {
            EndDisco();
            paperPlane.ResetPlane();
            paperPlane.turnOnDangerZones();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            disco.SetActive(true);
            DisableCollider();

        }
    }

    public void ResetDisco()
    {
        disco.SetActive(true);
        DisableCollider();
        foreach (Transform child in transform)
        {
            enemyNum++;
            child.gameObject.SetActive(true);
        }
    }

    public void EndDisco()
    {
        disco.SetActive(false);
    }

    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }

    public void DisableCollider()
    {
        boxCollider.enabled = false;
    }
}
