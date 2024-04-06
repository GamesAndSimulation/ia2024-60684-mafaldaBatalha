using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public Transform position;
    public MainSystemScript mainSystemScript;
    public Boolean resetPlane;
    public PaperPlane paperPlane;

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
        if(other.gameObject.tag == "Player")
        {
            mainSystemScript.removeLive();
            mainSystemScript.TeleportPlayer(position);
            if(resetPlane)
            {
                paperPlane.ResetPlane();
            }
        }
    }
}
