using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ShakedownScript : MonoBehaviour
{
    private CollectibleManagerTMP collectibleManager;
    private DialogueTrigger dialogueTrigger;
    private bool inZone = false;
    private int loot;
    public int upperBound = 201;
    public int lowerBound = 125;

    void Start()
    {
        collectibleManager = FindObjectOfType<CollectibleManagerTMP>();
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();
    }

    void Update()
    {
        if (inZone)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                loot = Random.Range(lowerBound, upperBound);
                collectibleManager.Collected(loot);
                this.gameObject.SetActive(false);
            }

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
