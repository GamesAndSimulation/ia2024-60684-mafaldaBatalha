using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeCollectible : MonoBehaviour
{
    public MainSystemScript mainSystemScript;
    public float lives;
    public Boolean isLimited;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainSystemScript.addLives(lives);
            if (isLimited)
                gameObject.SetActive(false);
        }
    }
}
