using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MainSystemScript : MonoBehaviour
{
    public GameObject model, camera;   
    public CharacterController characterController;
    public float speed, jumpSpeed;
    public DiscoSystem discoSystem;
    public PaperPlane paperPlane;

    public Transform position1, position2, position3;

    private float bounceSpeed, angle, turnSmoothVelocity, time, ySpeed, originalStepOffset;
    private float turnSmoothTime = 0.1f;
    private Vector3 direction, velocity;
    private Boolean bounced = false;


    // Start is called before the first frame update
    void Start()
    {
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");


        if (Input.GetKeyDown("1"))
        {
            discoSystem.ResetDisco();
            characterController.enabled = false;
            transform.position = position1.position;
            transform.rotation = position1.rotation;
            characterController.enabled = true;
        }

        if (Input.GetKeyDown("2"))
        {
            discoSystem.ResetDisco();
            characterController.enabled = false;
            transform.position = position2.position;
            transform.rotation = position2.rotation;
            characterController.enabled = true;
        }

        if (Input.GetKeyDown("3"))
        {
            transform.SetParent(null);
            discoSystem.EndDisco();
            paperPlane.ResetPlane();
            characterController.enabled = false;
            transform.position = position3.position;
            transform.rotation = position3.rotation;
            characterController.enabled = true;

        }


        //if the horizontal and vertical values are zero, it means the player is just standing, therefore I want them to face the camera
        if (horizontalValue == 0 && verticalValue == 0)
        {
            time += Time.deltaTime;
            if (time > 10)
            {
                angle = 180;
            }
            velocity = new Vector3(0,0,0);

        }
        //calculate the angle of the player depending on their direction
        else
        {
            time = 0;
            angle = Mathf.Atan2(horizontalValue, verticalValue) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;

            //vector that represents the direction in which the player is moving based on the input of the arrow/WASD keys and the camera
            direction = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

           
            //the direction vector is normalized, because otherwise the player would move too quickly diagonally
            velocity = direction.normalized * speed;

        }

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded){

            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;


            if (Input.GetButton("Jump"))
            {
                ySpeed = jumpSpeed;
            }
            else if(bounced)
            {
                ySpeed = bounceSpeed;
                bounced = false;
            }
        }
        else
        {
            characterController.stepOffset = 0f;
        }

        velocity.y = ySpeed;

        //move the character based on the direction of the input
        characterController.Move(velocity * Time.deltaTime);

        //applied SmoothDampAngle to the previously defined angle in order to make the turn smoother
        angle = Mathf.SmoothDampAngle(model.transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);

        //rotate the model of the player on the y axis according 
        model.transform.eulerAngles = new Vector3(0f, angle, 0f);
    }

    public void Bounced(float bounceSpeed)
    {
        this.bounceSpeed = bounceSpeed;
        bounced = true;
    }

    
   
}
