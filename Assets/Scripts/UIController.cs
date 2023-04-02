using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardinalDirectionText;

    void Start()
    {
        PlayerController playerEvents = GameObject.Find("Player").GetComponent<PlayerController>();
        playerEvents.OnCardinalChanged += UpdateCardinalDirection;
    }

    void UpdateCardinalDirection(object sender, PlayerController.CardinalDirection e)
    {
        cardinalDirectionText.text = e.ToString();
    }
}
