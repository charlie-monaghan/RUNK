using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EndingCheck : MonoBehaviour
{
    public CollectibleManagerTMP collectibleManager;
    public Transform BestEndingParent;
    public Transform GoodEndingParent;
    public Transform BadEndingParent;
    public Transform WorstEndingParent;
    private GameObject textToScroll;
    private Animator textAnimator;

    [SerializeField] private int winThreshold = 15000;


    void Start()
    {

        if (collectibleManager.ReturnCollected() < 0) // if chips is negative
        {
            //Worst ending
            PlayEnding(WorstEndingParent.gameObject);
        }
        else if (collectibleManager.ReturnCollected() == 2147483647) // if only the one tru chip was picked up
        {
            //Best ending
            PlayEnding(BestEndingParent.gameObject);
        }
        else if (collectibleManager.ReturnCollected() >= winThreshold) // if they got the win threshold or more
        {
            //Good ending
            PlayEnding(GoodEndingParent.gameObject);
        }
        else if (collectibleManager.ReturnCollected() < winThreshold && collectibleManager.ReturnCollected() >= 0) //  if they didn't make the win threshold
        {
            //Bad Ending 
            PlayEnding(BadEndingParent.gameObject);
        }

    }

    void PlayEnding(GameObject endingGO) { 
        endingGO.SetActive(true);
        textAnimator = endingGO.GetComponentInChildren<Animator>();
        textAnimator.Play("text scroll", 0, 0);
    }
}
