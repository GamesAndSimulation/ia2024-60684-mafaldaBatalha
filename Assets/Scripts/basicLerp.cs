using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicLerp : MonoBehaviour
{
    public Vector3 startPos, endPos;
    public float speed = 0.5f;  // 2 secondes

    private float startTime, totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) * speed;
        if (t <= 1)
            transform.position = Vector3.Lerp(startPos, endPos, t);
    }
}
