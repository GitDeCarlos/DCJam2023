using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] Vector3 mazeOffset;
    [SerializeField] float nodeSize;
    List<MazeNode> nodes = new List<MazeNode>();

    private void Start()
    {
        GenerateMazeInstant(mazeSize, mazeOffset);
        CreateExit(mazeSize, mazeOffset);
        
    }

    void GenerateMazeInstant(Vector2Int size, Vector3 offset)
    {
        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f)) * nodeSize;
                MazeNode newNode = Instantiate(nodePrefab, nodePos + offset, Quaternion.identity, transform);
                newNode.pos = nodePos;
                nodes.Add(newNode);
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose starting node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        //currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                //chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                //currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }

    void CreateExit(Vector2Int size, Vector3 offset)
    {
        // 1 North: max Z
        // 2 South: 0 Z
        // 3 East: max X
        // 4 West: 0 X
        int direction = Random.Range(0, 4);
        int x;
        int z;

        switch(direction+1){
            case 1:
                x = EvenRandom(-size.x, size.x);
                z = size.y-2;

                SearchForNode(x,z).RemoveWall(2);
                break;
            case 2:
                x = EvenRandom(-size.x, size.x);
                z = -size.y;

                SearchForNode(x,z).RemoveWall(3);
                break;
            case 3:
                x = size.x-2;
                z = EvenRandom(-size.y, size.y);

                SearchForNode(x,z).RemoveWall(0);
                break;
            case 4:
                x = -size.x;
                z = EvenRandom(-size.y, size.y);

                SearchForNode(x,z).RemoveWall(1);
                break;
        }
    }

    int EvenRandom(int min, int max)
    {
        int randint = Random.Range(min, max);
        while(randint % 2 != 0) {
            randint = Random.Range(min, max);
        }

        return randint; 
    }

    MazeNode SearchForNode(int x, int z)
    {
        for(int i = 0; i < nodes.Count; i++){
            if(nodes[i].pos.x == x && nodes[i].pos.z == z){
                return nodes[i];
            }
        }

        return null;
    }
}