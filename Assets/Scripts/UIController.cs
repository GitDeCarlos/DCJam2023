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
    [SerializeField] GameObject encounterUIContainer;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                ClearUI();
                encounterController.EndEncounter();
                isUIActive = false;
            }
        }
    }

    private void ClearUI()
    {

        encounterUIContainer.SetActive(false);
    }

    private void ShowEncounterUI(object sender, EncounterScriptableObject e)
    {
        isUIActive = true;

        encounterUIContainer.SetActive(true);

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
