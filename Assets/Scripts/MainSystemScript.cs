using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MainSystemScript : MonoBehaviour
{
    public GameObject model, camera, redTint;   
    public CharacterController characterController;
    public SceneManagerScript sceneManagerScript;
    public float speed, jumpSpeed, lives;
    public DiscoSystem discoSystem;
    public PaperPlane paperPlane;
    public String gameOverScene;

    public Transform position1, position2, position3, position4, position5;

    private float bounceSpeed, angle, turnSmoothVelocity, time, ySpeed, originalStepOffset;
    private float turnSmoothTime = 0.1f;
    private Vector3 direction, velocity;
    private Boolean bounced;


    // Start is called before the first frame update
    void Start()
    {
        originalStepOffset = characterController.stepOffset;
        bounced = false;
        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");


        if (Input.GetKeyDown("1"))
        {
            transform.SetParent(null);
            discoSystem.EndDisco();
            discoSystem.EnableCollider();
            paperPlane.ResetPlane();
            paperPlane.turnOffDangerZones();
            TeleportPlayer(position1);
        }

        if (Input.GetKeyDown("2"))
        {
            transform.SetParent(null);
            discoSystem.ResetDisco();
            paperPlane.ResetPlane();
            paperPlane.turnOffDangerZones();
            TeleportPlayer(position2);
        }

        if (Input.GetKeyDown("3"))
        {
            transform.SetParent(null);
            discoSystem.EndDisco();
            paperPlane.ResetPlane();
            paperPlane.turnOnDangerZones();
            TeleportPlayer(position3);

        }

        if (Input.GetKeyDown("4"))
        {
            transform.SetParent(null);
            discoSystem.EndDisco();
            TeleportPlayer(position4);

        }

        if (Input.GetKeyDown("5"))
        {
            transform.SetParent(null);
            discoSystem.EndDisco();
            TeleportPlayer(position5);
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

    public void TeleportPlayer(Transform position)
    {
        characterController.enabled = false;
        transform.position = position.position;
        transform.rotation = position.rotation;
        characterController.enabled = true;
    }

    public void removeLive()
    {
        lives--;
        redTint.SetActive(true);
        StartCoroutine(waiter(0.7f));

        if (lives <= 0)
            LoadScene(gameOverScene);
        
    }

    public void addLive()
    {
        lives++;
    }

    private void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(10, 50, 400, 100), "Lives: " + lives);
    }

    public void LoadScene(String sceneName)
    {
        sceneManagerScript.LoadScene(sceneName);
    }

    IEnumerator waiter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        redTint.SetActive(false);
    }

}
