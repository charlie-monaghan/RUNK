using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWall : MonoBehaviour
{
    private bool inZone = false;
    public int clicksNeeded = 3;
    public GameObject behindWallObjects;
    int clicks;
    void Start()
    {
        clicks = 0;
        this.gameObject.SetActive(true);
        behindWallObjects.SetActive(false);

    }

    void Update()
    {
        if(inZone == true && Input.GetKeyDown(KeyCode.E))
        {
            clicks++;
        }
        if (clicks >= clicksNeeded)
        {
            this.gameObject.SetActive(false);
            behindWallObjects.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inZone = false;
        }
    }

}
