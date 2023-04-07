using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineTriggerController : MonoBehaviour
{
    
    enum EventState { Empty, Item, Encounter }
    ItemScriptableObject item;
    [SerializeField] EncounterScriptableObject encounter;
    EncounterController encounterController;
    PlayerController playerEvents;

    MeshRenderer meshRenderer;

    public Material emptyMaterial, itemMaterial, encounterMaterial;

    EventState eventState;

    bool isPlayerInPhase = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        EventState[] eventStates = (EventState[])Enum.GetValues(typeof(EventState));
        eventState = eventStates[UnityEngine.Random.Range(0, eventStates.Length)];

        // Connect event triggers
        playerEvents = GameObject.Find("Player").GetComponent<PlayerController>();
        playerEvents.OnPhaseEntered += ShowEventMaterial;
        playerEvents.OnPhaseExited += HideEventMaterial;

        encounterController = GameObject.Find("EncounterController").GetComponent<EncounterController>();

        switch(eventState)
        {
            case EventState.Empty:
                meshRenderer.material = emptyMaterial;
                break;
            case EventState.Item:
                meshRenderer.material = itemMaterial;
                break;
            case EventState.Encounter:
                meshRenderer.material = encounterMaterial;
                break;
        }
    }

    void ShowEventMaterial(object sender, EventArgs e)
    {
        if (eventState != EventState.Empty) meshRenderer.enabled = true;
        isPlayerInPhase = true;
    }

    void HideEventMaterial(object sender, EventArgs e)
    {
        meshRenderer.enabled = false;
        isPlayerInPhase = false;
    }

    void SetOffLandmine()
    {
        if (eventState != EventState.Empty) encounterController.TriggerEncounter(encounter);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isPlayerInPhase) SetOffLandmine();
    }
}
