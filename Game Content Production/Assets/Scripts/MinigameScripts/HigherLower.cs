using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HigherLower : MonoBehaviour
{
    //Maybe change to where player can put up a certain amount of chips to wager instead of it being a flat cost
    private CollectibleManagerTMP collectibleManager;
    public TextMeshProUGUI givenNumber;
    public Transform UI;
     public float unsafeBetMult = 3f;
    public float safeBetMult = 1.2f;
    public int cost = 20;

    private bool activeGame = false;
    private int prize;
    private bool inZone = false;
    private int guess;

    private int rand;
    private int given = 0;
    
    void Start()
    {
        collectibleManager = FindObjectOfType<CollectibleManagerTMP>();
        givenNumber.text = "";
        //UI.gameObject.SetActive(false);
    }


    void Update()
    {
        if (inZone == true && activeGame == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("In if inzone, activegame false, recieved E input");
            if (!UI.gameObject.activeInHierarchy) 
            {
                GenerateGiven();
            }
            UI.gameObject.SetActive(true);
            if(UI != null)
            {
                Debug.Log("UI is there");
            }

            if (collectibleManager.ReturnCollected() >= cost)
            {
                //collectibleManager.Deduct(cost);
                //activeGame = true;
                //gameLogic();
            }
            else
            {
                //FIXME: show up as dialogue with Randy saying something along the lines of "Damn, not enough chips"
                Debug.Log("Too Broke");
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
            givenNumber.text = "";
            UI.gameObject.SetActive(false);
            guess = 0;
        }
    }

    private void gameLogic()
    {

        //if player guesses lower and is correct
        if (guess == -1 && rand < given)
        {
            if (given < 50)
            {
                //Player took unsafe bet
                prize = Mathf.RoundToInt(cost * unsafeBetMult);
                collectibleManager.Collected(prize);
                GenerateGiven();
            }
            else if(given >= 50)
            {
                //player took safe bet
                prize = Mathf.RoundToInt(cost * safeBetMult);
                collectibleManager.Collected(prize);
                GenerateGiven();
            }
            else
            {
                Debug.Log("Something is amiss...");
            }

            Debug.Log("Guessed lower, correct");
            activeGame = false;
        }
        //if player guesses higher and is correct
        else if (guess == 1 && rand > given)
        {
            if (given < 50)
            {
                //Player took safe bet
                prize = Mathf.RoundToInt(cost * safeBetMult);
                collectibleManager.Collected(prize);
                GenerateGiven();
            }
            else if (given >= 50)
            {
                //player took safe bet
                prize = Mathf.RoundToInt(cost * unsafeBetMult);
                collectibleManager.Collected(prize);
                GenerateGiven();
            }
            else
            {
                Debug.Log("Something is amiss...");
            }
            Debug.Log("Guessed higher, correct");
            activeGame = false;
        }
        //if player guess is incorrect
        else
        {
            Debug.Log("Lost the bet");
            activeGame = false;
            GenerateGiven();
        }

    }

    private void GenerateGiven()
    {
        //generates the hidden and the given number within a range
        rand = Random.Range(1, 101);
        given = Random.Range(25, 76);

        //if the two numbers are the same, regenerates the random one
        while (rand == given)
        {
            rand = Random.Range(1, 101);
        }

        givenNumber.text = "" + given;
    }

    public void HigherButton()
    {
        if (collectibleManager.ReturnCollected() >= cost)
        {
            guess = 1;
            collectibleManager.Deduct(cost);
            activeGame = true;
            gameLogic();
        }
    }

    public void LowerButton() 
    {
        if (collectibleManager.ReturnCollected() >= cost)
        {
            guess = -1;
            collectibleManager.Deduct(cost);
            activeGame = true;
            gameLogic();
        }
    }

    public void QuitButton()
    {
        UI.gameObject.SetActive(false);
    }

    private int TestGuess()
    {
        // -1 player guess lower, 1 player guess higher
        return  Random.Range(0,2) == 0 ? -1 : 1;
    }

}
