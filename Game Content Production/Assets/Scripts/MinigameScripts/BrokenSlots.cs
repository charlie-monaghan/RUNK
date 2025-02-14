using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class BrokenSlots : MonoBehaviour
{
    private bool activeGame = false;
    private bool inZone = false;
    private CollectibleManagerTMP collectibleManager;
    public int cost;
    public Transform buttons;
    public TextMeshProUGUI leftNumber;
    public TextMeshProUGUI middleNumber;
    public TextMeshProUGUI rightNumber;

    void Start()
    {
        collectibleManager = FindObjectOfType<CollectibleManagerTMP>();
        buttons.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inZone == true && activeGame == false && Input.GetKeyDown(KeyCode.E))
        {
            buttons.gameObject.SetActive(true);
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
            buttons.gameObject.SetActive(false);
        }
    }

    public void BrokenPlayButton()
    {
        if (collectibleManager.ReturnCollected() >= cost)
        {
            collectibleManager.Deduct(cost);
            activeGame = true;
            gameLogic();
        }
        else if(collectibleManager.ReturnCollected() < cost)
        {
            collectibleManager.EmptyChips();
        }
        else
        {
            //FIXME: show up as dialogue with Randy saying something along the lines of "Damn, not enough chips"
            Debug.Log("Too Broke");
        }
    }

    public void BrokenQuitButton()
        {
            buttons.gameObject.SetActive(false);

        }

    public void gameLogic()
    {
        int[] slotResults = new int[3];
        for (int i = 0; i < slotResults.Length; i++)
        {
            slotResults[i] = Random.Range(0, 10);
        }

        leftNumber.text = slotResults[0].ToString();
        middleNumber.text = slotResults[1].ToString();
        rightNumber.text = slotResults[2].ToString();
        activeGame = false;
    }

}
