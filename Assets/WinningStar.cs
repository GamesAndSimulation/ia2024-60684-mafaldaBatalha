using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningStar : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MainSystemScript mainSystemScript;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            meshRenderer.enabled = false;
            mainSystemScript.LoadScene(sceneName);
        }
        
        
    }
}
