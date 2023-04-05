using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState { Available, Current, Completed }

public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] GameObject landmineTrigger;
    [SerializeField] MeshRenderer floor;
    [SerializeField] Material lightMaterial;
    [SerializeField] Material darkMaterial;

    MeshRenderer meshRenderer;
    PlayerController playerEvents;

    void Start()
    {
        foreach (GameObject gameObject in walls)
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = lightMaterial;
            }
        }

        Instantiate(landmineTrigger, transform);

        playerEvents = GameObject.Find("Player").GetComponent<PlayerController>();
        playerEvents.OnPhaseEntered += ChangeToPhasedMaterial;
        playerEvents.OnPhaseExited += ChangeToLightMaterial;
    }

    private void ChangeToLightMaterial(object sender, EventArgs e)
    {
        foreach (GameObject gameObject in walls)
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = lightMaterial;
            }
        }
        //floor.material = lightMaterial;
    }

    private void ChangeToPhasedMaterial(object sender, EventArgs e)
    {
        foreach (GameObject gameObject in walls)
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = darkMaterial;
            }
        }
        //loor.material = darkMaterial;
    }

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                floor.material.color = Color.white;
                break;
            case NodeState.Current:
                floor.material.color = Color.yellow;
                break;
            case NodeState.Completed:
                floor.material.color = Color.blue;
                break;
        }
    }
}