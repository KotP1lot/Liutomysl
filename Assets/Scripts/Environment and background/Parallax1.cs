using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax1 : MonoBehaviour
{
    float length = 17.92f;
    private Vector2 startPos;
    public GameObject cam;
    public float parallaxEffect;
    public float elevationEffect;

    public GameObject childLayer;
    Transform[] childObjects;
    List<Vector3> childStartPos = new List<Vector3>();

    void Start()
    {
        startPos = transform.position;
        if(childLayer != null)
        {
            childObjects = childLayer.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in childObjects)
            {
                childStartPos.Add(child.position);
            }
        }
    }

    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float elevation = cam.transform.position.y * elevationEffect;
        transform.position = new Vector3(startPos.x + distance, startPos.y + elevation, transform.position.z);

        if (childLayer != null)
        {
            for(int i = 0; i < childObjects.Length; i++)
            {
                var child = childObjects[i];
                var childPos = childStartPos[i];
                child.position = new Vector3(childPos.x + distance, childPos.y + elevation, transform.position.z);
            }
        }

        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        if (temp > startPos.x + length)
        {
            startPos.x += length;
        }
        else if(temp < startPos.x - length)
        {
            startPos.x -= length;
        }
    }
}
