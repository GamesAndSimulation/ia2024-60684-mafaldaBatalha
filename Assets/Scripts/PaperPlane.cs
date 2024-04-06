using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PaperPlane : MonoBehaviour
{
    public Transform position1, position2;
    public basicLerp basicLerp;
    public GameObject dangerZones;

    private Boolean hasMoved;

    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlane()
    {
        basicLerp.enabled = false;
        hasMoved = false;
        transform.position = position1.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(transform);

            if(!hasMoved)
            {
                hasMoved = true;
                basicLerp.enabled = true;
                basicLerp.ResetLerp();
                
            }
            
        }
        
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }

    public void turnOffDangerZones()
    {
        dangerZones.SetActive(false);
    }

    public void turnOnDangerZones()
    {
        dangerZones.SetActive(true);
    }
}
