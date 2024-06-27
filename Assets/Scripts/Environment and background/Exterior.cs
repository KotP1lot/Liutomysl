using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exterior : MonoBehaviour
{
    public GameObject front_layer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(fadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(fadeIn());
        }
    }
    private List<Transform> getChildren()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        if (front_layer != null) 
        {
            foreach (Transform child in front_layer.transform)
            {
                children.Add(child);
            }
        }
        
        return children;
    }


    private IEnumerator fadeOut()
    {
        var children = getChildren();
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            foreach (Transform child in children)
            {
                Color c = child.GetComponent<SpriteRenderer>().material.color;
                c.a = f;
                child.GetComponent<SpriteRenderer>().material.color = c;
                
            }
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    private IEnumerator fadeIn()
    {
        var children = getChildren();
        for (float f = 0.05f; f <= 1.05f; f += 0.05f)
        {
            foreach (Transform child in children)
            {
                Color c = child.GetComponent<SpriteRenderer>().material.color;
                c.a = f;
                child.GetComponent<SpriteRenderer>().material.color = c;
                
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
