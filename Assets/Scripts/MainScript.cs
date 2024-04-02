using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public int bullets = 20;
    public GameObject myTree;

    public GameObject projectile;
    public float fireforce = 1000;

    public float moveSpeed = 2;
    public float jumpForce = 20;


    // Start is called before the first frame update
    void Start()
    {
        //How to add an object to a node
        Instantiate(myTree, new Vector3(4, 0, -4), Quaternion.identity, GameObject.Find("Trees").transform);


        /* Comment to unlock the mouse cursor */
        Cursor.lockState = CursorLockMode.Confined; 
        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        float horizontalMouseValue = Input.GetAxis("Mouse X");
        float verticalMouseValue = Input.GetAxis("Mouse Y");


        Rigidbody rb = GetComponent<Rigidbody>();

        //two ways of jumping
        if ( Input.GetButton("Jump") )   //Input.GetKey("space")
            rb.AddForce(new Vector3(0, jumpForce, 0));
    
        transform.position += transform.forward * verticalValue* moveSpeed * Time.deltaTime + transform.right * horizontalValue* moveSpeed * Time.deltaTime;

        //naive version - does not depend on time, doesnt use the correct mouse position
        transform.eulerAngles += new Vector3(-verticalMouseValue , horizontalMouseValue, 0);


        if (Input.GetButtonUp("Fire1") && bullets > 0)
        {
            GameObject instantiatedBullet =
                Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            instantiatedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * fireforce);

            Destroy(instantiatedBullet, 5);
            bullets--;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Add bullets"))
        {
            Debug.Log("Add 10 bullets");
            bullets += 10+1;  //plus the one that is fired
        }

        GUI.contentColor = Color.red;
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(10, 50, 400, 100), "bullets " + bullets);
    }
}
