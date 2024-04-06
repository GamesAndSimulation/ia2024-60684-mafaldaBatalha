using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoSystem : MonoBehaviour
{
    public MainSystemScript mainSystemScript;
    public GameObject discoBall, disco, enemies, discoCollectibles, bookCollectibles;
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
            bookCollectibles.SetActive(true);
            mainSystemScript.AudioOn();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            mainSystemScript.AudioOff();
            disco.SetActive(true);
            DisableCollider();

        }
    }

    public void ResetDisco()
    {
        mainSystemScript.AudioOff();
        disco.SetActive(true);
        DisableCollider();
        for(int i = 0; i < enemies.transform.childCount; i++)
        {
            enemyNum++;
            enemies.transform.GetChild(i).gameObject.SetActive(true);
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

    public void turnOnBookCollectibles()
    {
        for(int i=0; i<bookCollectibles.transform.childCount; i++)
        {
            bookCollectibles.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void turnOffBookCollectibles()
    {
        bookCollectibles.SetActive(false);
    }
}
