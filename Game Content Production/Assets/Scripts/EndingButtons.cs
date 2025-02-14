using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttons;

    void ActivateButtons()
    {
        buttons.SetActive(true);
    }

    void Start()
    {
        buttons.SetActive(false);
    }
}
