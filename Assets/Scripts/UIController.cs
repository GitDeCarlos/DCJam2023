using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardinalDirectionText;
    [SerializeField] TextMeshProUGUI phaseStateText;

    [Header("Encounter UI Components")]
    [SerializeField] Image encounterUIPanel;
    [SerializeField] Image encounterUIImage;
    [SerializeField] TextMeshProUGUI encounterUIText;

    PlayerController playerEvents;
    EncounterController encounterController;

    // States
    bool isUIActive = false;

    void Start()
    {
        playerEvents = GameObject.Find("Player").GetComponent<PlayerController>();
        playerEvents.OnCardinalChanged += UpdateCardinalDirection;
        playerEvents.OnPhaseChanged += UpdatePhaseState;

        encounterController = GameObject.Find("EncounterController").GetComponent<EncounterController>();
        encounterController.OnEncounterTrigger += ShowEncounterUI;

        ClearUI();
    }

    void Update()
    {
        if (isUIActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClearUI();
                encounterController.EndEncounter();
                isUIActive = false;
            }
        }
    }

    private void ClearUI()
    {
        encounterUIPanel.enabled = false;
        encounterUIImage.enabled = false;
        encounterUIText.enabled = false;
    }

    private void ShowEncounterUI(object sender, EncounterScriptableObject e)
    {
        isUIActive = true;

        encounterUIPanel.enabled = true;
        encounterUIImage.enabled = true;
        encounterUIText.enabled = true;
;
        encounterUIImage.sprite = e.encounterSprite;
        encounterUIText.text = "Found " + e.encounterType + ": " + e.name;
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
