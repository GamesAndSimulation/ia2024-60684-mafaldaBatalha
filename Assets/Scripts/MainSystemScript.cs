using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MainSystemScript : MonoBehaviour
{
    public GameObject model, camera, redTint, collectibles, music;   
    public CharacterController characterController;
    public SceneManagerScript sceneManagerScript;
    public float speed, jumpSpeed, livesNum;
    public DiscoSystem discoSystem;
    public PaperPlane paperPlane;
    public String gameOverScene;
    public Font font;
    public Animator animator;

    public Transform position1, position2, position3, position4, position5;

    private float bounceSpeed, angle, turnSmoothVelocity, time, ySpeed, originalStepOffset, lives;
    private float turnSmoothTime = 0.1f;
    private Vector3 direction, velocity;
    private Boolean bounced, isJumping, isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        originalStepOffset = characterController.stepOffset;
        bounced = false;
        lives = livesNum;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");


        if (Input.GetKeyDown("c"))
        {
            lives = livesNum;
        }

        if (Input.GetKeyDown("1"))
        {
            transform.SetParent(null);
            if (music.activeSelf == false)
                AudioOn();
            collectibles.SetActive(true);
            discoSystem.EndDisco();
            discoSystem.EnableCollider();
            paperPlane.ResetPlane();
            paperPlane.turnOffDangerZones();
            TeleportPlayer(position1);
        }

        if (Input.GetKeyDown("2"))
        {  
            transform.SetParent(null);
            collectibles.SetActive(true);
            AudioOff();
            discoSystem.turnOffBookCollectibles();
            discoSystem.ResetDisco();
            paperPlane.ResetPlane();
            paperPlane.turnOffDangerZones();
            TeleportPlayer(position2);
        }

        if (Input.GetKeyDown("3"))
        {
            transform.SetParent(null);
            collectibles.SetActive(true);
            if (music.activeSelf == false)
                AudioOn();
            discoSystem.EndDisco();
            discoSystem.turnOnBookCollectibles();
            discoSystem.DisableCollider();
            paperPlane.ResetPlane();
            paperPlane.turnOnDangerZones();
            TeleportPlayer(position3);

        }

        if (Input.GetKeyDown("4"))
        {
            transform.SetParent(null);
            collectibles.SetActive(true);
            if (music.activeSelf == false)
                AudioOn();
            discoSystem.DisableCollider();
            discoSystem.EndDisco();
            TeleportPlayer(position4);

        }

        if (Input.GetKeyDown("5"))
        {
            transform.SetParent(null);
            collectibles.SetActive(true);
            if (music.activeSelf == false)
                AudioOn();
            discoSystem.EndDisco();
            TeleportPlayer(position5);
        }


        //if the horizontal and vertical values are zero, it means the player is just standing, therefore I want them to face the camera
        if (horizontalValue == 0 && verticalValue == 0)
        {
            animator.SetBool("isMoving", false);
            time += Time.deltaTime;
            if (time > 10)
            {
                angle = 180;
                animator.SetBool("isIdle", true);
            }
            else
            {
                animator.SetBool("isIdle", false);
            }
            velocity = new Vector3(0,0,0);

        }
        //calculate the angle of the player depending on their direction
        else
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);
            time = 0;
            angle = Mathf.Atan2(horizontalValue, verticalValue) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;

            //vector that represents the direction in which the player is moving based on the input of the arrow/WASD keys and the camera
            direction = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

           
            //the direction vector is normalized, because otherwise the player would move too quickly diagonally
            velocity = direction.normalized * speed;

        }

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded){

            animator.SetBool("isGrounded", true);
            isGrounded = true;

            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);


            if (Input.GetButton("Jump"))
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJumping = true;
            }
            else if(bounced)
            {
                ySpeed = bounceSpeed;
                bounced = false;
                animator.SetBool("isJumping", true);
                isJumping = true;
            }

            
        }
        else
        {
            animator.SetBool("isGrounded", false);
            isGrounded = false;
            characterController.stepOffset = 0f;

            if((ySpeed < 0 && isJumping) || ySpeed < 2)
            {
                animator.SetBool("isFalling", true);
            }
        }

        velocity.y = ySpeed;

        //move the character based on the direction of the input
        characterController.Move(velocity * Time.deltaTime);

        //applied SmoothDampAngle to the previously defined angle in order to make the turn smoother
        angle = Mathf.SmoothDampAngle(model.transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);

        //rotate the model of the player on the y axis according 
        model.transform.eulerAngles = new Vector3(0f, angle, 0f);
    }

    public void AudioOn()
    {
        music.SetActive(true);
    }

    public void AudioOff()
    {
        music.SetActive(false);
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

    public void addLives(float lives)
    {
        this.lives += lives;
        if(this.lives > livesNum)
        {
            this.lives = livesNum;
        }
    }

    private void OnGUI()
    {
        
        GUI.contentColor = Color.white;
        GUI.skin.label.font = font;
        GUI.skin.label.fontSize = 25;
        GUI.Label(new Rect(10, 5, 400, 100), "Lives: " + lives);
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
