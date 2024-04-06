using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public DiscoSystem discoSystem;
    public List<Transform> positions;
    public float speed, totalLives;

    private Transform startPos, endPos;
    private float startTime, totalDistance;
    private int index = 0;
    private float currentLives;

    //private float t; //alternative version

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        if (positions.Count >= 2)
            updateIndexes();
        currentLives = totalLives;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentLives == 0)
        {
            discoSystem.KillAnEnemy();
            gameObject.SetActive(false);
        }

        float t = (Time.time - startTime) * speed;
        //t += Time.deltaTime;  //alternative
        if (t > 1)
        {
            updateIndexes();
            t -= 1;
            startTime = Time.time;
        }
        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
        transform.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, t);
        //transform.localScale = Vector3.Lerp(startPos.localScale, endPos.localScale, t);

    }
    //updates the start and end positions of the lerp
    void updateIndexes()
    {
        if (index == positions.Count - 1) //reached the end
        {
            startPos = positions[index];
            endPos = positions[0];
            index = 0;
        }
        else
        {
            startPos = positions[index];
            endPos = positions[index + 1];
            index++;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            currentLives--;
        }
            
    }


}
