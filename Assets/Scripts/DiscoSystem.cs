using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoSystem : MonoBehaviour
{

    public GameObject discoBall;
    public GameObject disco;
    public Collider boxCollider;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the disco ball rotate
        discoBall.transform.eulerAngles = new Vector3(0f, speed * Time.time, 0f);
    }

    IEnumerator FadeOutAndDestroy(float time)
    {
        float elapsedTime = 0;
        Color startingColor = disco.transform.GetComponent<Renderer>().material.color;
        Color finalColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0);
        while (elapsedTime < time)
        {
            disco.transform.GetComponent<Renderer>().material.color = Color.Lerp(startingColor, finalColor, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    IEnumerator FadeIn(float time)
    {
        float elapsedTime = 0;
        
        Color finalColor = disco.transform.GetComponent<Renderer>().material.color;
        Color startingColor = new Color(finalColor.r, finalColor.g, finalColor.b, 0);

        while (elapsedTime < time)
        {
            disco.transform.GetComponent<Renderer>().material.color = Color.Lerp(startingColor, finalColor, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(true);
        boxCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
   
            disco.SetActive(true);
            boxCollider.enabled = false;

            //StartCoroutine(FadeIn(5));
        }
    }
}
