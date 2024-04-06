using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DiscoCube : MonoBehaviour
{
    public CharacterController characterController;
    public float bounceSpeed;
    public  MainSystemScript mainSystemScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainSystemScript.Bounced(bounceSpeed);
           
        }

    }



}
