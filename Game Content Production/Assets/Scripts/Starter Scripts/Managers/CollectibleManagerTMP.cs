using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManagerTMP : MonoBehaviour
{
    [SerializeField] private TMP_Text collectibles;
    [SerializeField] private int collected;
    public static int chipTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectibles.text = "" + chipTotal;
    }

    public void Collected(int value)
    {
        collected += value;
        chipTotal += value;

    }

    public void Deduct(int value)
    {
        collected = Mathf.Max(0, collected - value);
        chipTotal -= value;
    }

    public int ReturnCollected()
    {
        return chipTotal;
    }

    public void EmptyChips()
    {
        chipTotal = 0;
    }

}
