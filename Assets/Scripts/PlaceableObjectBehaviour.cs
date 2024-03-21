using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectBehaviour : MonoBehaviour
{
    public Camera camera; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if( Physics.Raycast(ray, out hitInfo))
        {
            transform.position = hitInfo.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }
}
