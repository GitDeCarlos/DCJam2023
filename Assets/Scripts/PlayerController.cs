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

    // Events
    public event EventHandler<CardinalDirection> OnCardinalChanged;

    void Start()
    {
        faceDirection = CardinalDirection.North;
        OnCardinalChanged?.Invoke(this, faceDirection);
    }

    void Update()
    {
        HandleTurnInput();
        HandleMoveInput(); // TEMP NOT FINAL
    }

    void HandleMoveInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsWallAhead()) return;
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
}
