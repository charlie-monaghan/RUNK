using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotsScript : MonoBehaviour
{
    //FIXME: eventually make a UI interface to show the player what the hell is going on
    private bool activeGame = false;
    private bool inZone = false;
    private CollectibleManagerTMP collectibleManager;
    public int jackpotMultiplier = 5;
    public int partialWinMultiplier = 2;
    public int cost = 20;
    private int prize = 0;
    public Transform buttons;
    public TextMeshProUGUI leftNumber;
    public TextMeshProUGUI middleNumber;
    public TextMeshProUGUI rightNumber;


    void Start()
    {
        collectibleManager = FindObjectOfType<CollectibleManagerTMP>();
        buttons.gameObject.SetActive(false);
    }

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
            inZone=false;
            buttons.gameObject.SetActive(false);
        }
    }

    public void PlayButton()
    {
        if (collectibleManager.ReturnCollected() >= cost)
        {
            collectibleManager.Deduct(cost);
            activeGame = true;
            gameLogic();
        }
        else
        {
            //FIXME: show up as dialogue with Randy saying something along the lines of "Damn, not enough chips"
            Debug.Log("Too Broke");
        }

    }

    public void QuitButton()
    {
        buttons.gameObject.SetActive(false);
    }

    public void gameLogic()
    {
        int[] slotResults = new int[3];
        for(int i = 0; i < slotResults.Length; i++)
        {
            slotResults[i] = Random.Range(0, 10);
        }

        leftNumber.text = slotResults[0].ToString();
        middleNumber.text = slotResults[1].ToString();
        rightNumber.text = slotResults[2].ToString();

        if (slotResults[0] == slotResults[1] && slotResults[1] == slotResults[2])
        {
            Debug.Log(slotResults[0] + " " + slotResults[1] + " " + slotResults[2] + " jackpot case");
            prize = cost + (slotResults[0] * slotResults[1] * slotResults[2]) * jackpotMultiplier;
            collectibleManager.Collected(prize);
            activeGame = false;
        }
        else if(slotResults[0] == slotResults[1] || slotResults[1] == slotResults[2] || slotResults[0] == slotResults[2])
        {
            Debug.Log(slotResults[0] + " " + slotResults[1] + " " + slotResults[2] + " partial win case");
            activeGame = false;
            if (slotResults[0] == slotResults[1])
            {
                prize = (cost + (slotResults[0] * slotResults[1])) * partialWinMultiplier;
                collectibleManager.Collected(prize);
                activeGame = false;
            }
            else if (slotResults[1] == slotResults[2])
            {
                prize = (cost + (slotResults[1] * slotResults[2])) * partialWinMultiplier;
                collectibleManager.Collected(prize);
                activeGame = false;
            }
            else if (slotResults[0] == slotResults[2])
            {
                prize = (cost + (slotResults[0] * slotResults[2])) * partialWinMultiplier;
                collectibleManager.Collected(prize);
                activeGame = false;
            }
            else
            {
                Debug.Log("Something is very wrong, and I don't really know what, if you see this.");
                activeGame = false;
            }
        }
        else
        {
            Debug.Log(slotResults[0] + " " + slotResults[1] + " " + slotResults[2] + " loss case");
            activeGame = false;
        }
    }
}
