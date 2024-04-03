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
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        //if the horizontal and vertical values are zero, it means the player is just standing, therefore I want them to face the camera
        if (horizontalValue == 0 && verticalValue == 0)
            angle = 180;
        else 
        {
            //calculates the angle of the rotation of the model on the y axis based on the input of the arrow keys
             angle = (float)((180 / Math.PI) * Math.Atan(horizontalValue / verticalValue));

            //adjust the angle when pressing the arrow down
            if (verticalValue < 0)
                angle += 180;
                      
        }

        
        //rotate the model of the player on the y axis according to the direction they're walking towards
        model.transform.eulerAngles = new Vector3(0, angle, 0);
        /*
         * normalized vector that represents the direction of the player's movement (if the vector wasn't normalized, the player 
         * would move too quickly diagonally
         */
        direction = new Vector3 (horizontalValue, 0, verticalValue).normalized;

        transform.position += direction * speed * Time.deltaTime;
        


    }


}
