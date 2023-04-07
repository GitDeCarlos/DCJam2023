using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public event EventHandler<EncounterScriptableObject> OnEncounterTrigger;
    public event EventHandler OnEncounterEnded;

    EncounterScriptableObject currentEncounter;

    bool isEnounterInProgress = false;

    public void TriggerEncounter(EncounterScriptableObject encounter)
    {
        currentEncounter = encounter;
        isEnounterInProgress = true;
        OnEncounterTrigger?.Invoke(this, currentEncounter);
    }

    public void EndEncounter()
    {
        currentEncounter = null;
        isEnounterInProgress = false;
        OnEncounterEnded?.Invoke(this, EventArgs.Empty);
    }
}
