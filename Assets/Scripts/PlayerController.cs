using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float turnIncrement = 90f;
    [SerializeField] float speed = 1f;

    public InventoryObject inventory;

    // Cardinal States
    public enum CardinalDirection{ North, East, South, West}
    CardinalDirection faceDirection;

    // Phase States
    public enum PhaseState{ Light, Shadow }
    PhaseState phaseState;

    // Events
    public event EventHandler<CardinalDirection> OnCardinalChanged;
    public event EventHandler<PhaseState> OnPhaseChanged;

    void Start()
    {
        faceDirection = CardinalDirection.North;
        phaseState = PhaseState.Light;

        OnCardinalChanged?.Invoke(this, faceDirection);
        OnPhaseChanged?.Invoke(this, phaseState);
    }

    void Update()
    {
        HandleTurnInput();
        HandleMoveInput(); // TEMP NOT FINAL

        HandlePhaseInput();
    }

    void HandlePhaseInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (String.Compare(phaseState.ToString(), "Light") == 0) phaseState = PhaseState.Shadow;
            else phaseState = PhaseState.Light;

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

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item){
             inventory.AddItem(item.item, 1);
             Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
