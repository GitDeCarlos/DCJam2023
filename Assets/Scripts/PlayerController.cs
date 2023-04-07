using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float turnIncrement = 90f;
    [SerializeField] float speed = 1f;

    // Cardinal States
    public enum CardinalDirection{ North, East, South, West}
    CardinalDirection faceDirection;

    // Phase States
    public enum PhaseState{ Light, Shadow }
    PhaseState phaseState;

    // Player States
    public enum PlayerState{ Moving, InInventory, InEncounter }
    PlayerState playerState;

    // Events
    public event EventHandler<CardinalDirection> OnCardinalChanged;
    public event EventHandler<PhaseState> OnPhaseChanged;
    public event EventHandler OnPhaseEntered;
    public event EventHandler OnPhaseExited;
    void Start()
    {
        faceDirection = CardinalDirection.North;
        phaseState = PhaseState.Light;
        playerState = PlayerState.Moving;

        OnCardinalChanged?.Invoke(this, faceDirection);
        OnPhaseChanged?.Invoke(this, phaseState);

        EncounterController encounterController = GameObject.Find("EncounterController").GetComponent<EncounterController>();
        encounterController.OnEncounterTrigger += EncounterEntered;
        encounterController.OnEncounterEnded += EncounterExited;
    }

    void Update()
    {

        switch (playerState)
        {
            case PlayerState.Moving:
                HandleMoveInput();
                //HandleTurnInput();
                //HandlePhaseInput();
                break;
            case PlayerState.InEncounter:
                break;
        }
    }

    void HandleMoveState()
    {
        HandleMoveInput();
    }

    void HandlePhaseInput()
    {
        if (playerState != PlayerState.Moving) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (String.Compare(phaseState.ToString(), "Light") == 0)
            {
                phaseState = PhaseState.Shadow;
                OnPhaseEntered?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                phaseState = PhaseState.Light;
                OnPhaseExited?.Invoke(this, EventArgs.Empty);
            }
            
            OnPhaseChanged?.Invoke(this, phaseState);
        }
    }

    void HandleMoveInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (phaseState == PhaseState.Light && IsWallAhead()) return;
            transform.position += transform.forward * speed;
        }
        HandlePhaseInput();
        HandleTurnInput();

    }

    private void EncounterEntered(object sender, EncounterScriptableObject e)
    {
        playerState = PlayerState.InEncounter;
    }
    private void EncounterExited(object sender, EventArgs e)
    {
        playerState = PlayerState.Moving;
    }

    bool IsWallAhead()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1))
        {
            return true;
        }
        return false;
    }

    void HandleTurnInput()
    {
        // Turn Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnIncrement);

            // Update Cardinal Direction Enum
            if (String.Compare(faceDirection.ToString(), "North") == 0) faceDirection = CardinalDirection.West;
            else faceDirection--;

            // Invoke UI Update
            OnCardinalChanged?.Invoke(this, faceDirection);
        }
        // Turn Right
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnIncrement);

            // Update Cardinal Direction Enum
            if (String.Compare(faceDirection.ToString(), "West") == 0) faceDirection = CardinalDirection.North;
            else faceDirection++;

            // Invoke UI Update
            OnCardinalChanged?.Invoke(this, faceDirection);
        }
    }
}
