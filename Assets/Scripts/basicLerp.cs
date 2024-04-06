using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicLerp : MonoBehaviour
{
    public Transform startPos, endPos;
    public float speed = 0.5f;  // 2 secondes

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float t = (Time.time - startTime) * speed;
        if (t <= 1)
        {
            transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
            transform.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, t);
        }
    }

    public void ResetLerp()
    {
        startTime = Time.time;
    }
}
