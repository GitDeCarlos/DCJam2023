using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardinalDirectionText;
    [SerializeField] TextMeshProUGUI phaseStateText;

    void Start()
    {
        PlayerController playerEvents = GameObject.Find("Player").GetComponent<PlayerController>();
        playerEvents.OnCardinalChanged += UpdateCardinalDirection;
        playerEvents.OnPhaseChanged += UpdatePhaseState;
    }

    private void UpdatePhaseState(object sender, PlayerController.PhaseState e)
    {
        phaseStateText.text = e.ToString();
    }

    void UpdateCardinalDirection(object sender, PlayerController.CardinalDirection e)
    {
        cardinalDirectionText.text = e.ToString();
    }
}
