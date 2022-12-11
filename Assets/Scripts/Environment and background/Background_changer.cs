using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_changer : MonoBehaviour
{
    public List<GameObject> layerList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (GameObject layer in layerList)
            {
                layer.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (GameObject layer in layerList)
            {
                layer.SetActive(true);
            }
        }
    }
}
