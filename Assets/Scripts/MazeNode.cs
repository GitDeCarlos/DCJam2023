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
    public Vector3 pos;

    [SerializeField] DataScriptableObject materialData;

    MeshRenderer meshRenderer;
    PlayerController playerEvents;

    void Start()
    {
        foreach (GameObject gameObject in walls)
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = materialData.lightWallMaterial;
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
                meshRenderer.material = materialData.lightWallMaterial;
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
                meshRenderer.material = materialData.darkWallMaterial;
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