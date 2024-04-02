using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MainSystemScript : MonoBehaviour
{
    public GameObject camera;
    public GameObject model;
    public Rigidbody rigidbody;
    public float speed = 5.0f;
    public float jumpForce = 0.01f;
    float angle;
 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        float horizontalMouseValue = Input.GetAxis("Mouse X");
        float verticalMouseValue = Input.GetAxis("Mouse Y");

        if (horizontalValue == 0 && verticalValue == 0)
            angle = 0;
        else
        {
             angle = (float)((180 / Math.PI) * Math.Atan(horizontalValue / verticalValue));

            if (verticalValue >= 0)
            {
                angle += 180;
            }
        }
           
        model.transform.eulerAngles = new Vector3(0, angle, 0);
        transform.position += transform.forward * -verticalValue * speed * Time.deltaTime + transform.right * -horizontalValue * speed * Time.deltaTime;
        


        if (Input.GetButton("Jump"))

            rigidbody.AddForce(new Vector3(0, jumpForce, 0));
          
        
     
        camera.transform.eulerAngles += new Vector3(-verticalMouseValue, horizontalMouseValue, 0);

    }


}
