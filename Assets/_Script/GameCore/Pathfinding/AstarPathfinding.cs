using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class AstarPathfinding
{
    public static Dictionary<Vector3Int, GameObject> TilesetMap;
    public static HexGrid HexGrid;
    private static float[] terrainModifiers = { 1, 2, 3, 3, 99999999 };


    public static int GetDistance(Vector3Int positionA, Vector3Int positionB)
    {
        Vector3Int distance = positionA - positionB;
        return (int)((MathF.Abs(distance.x) + MathF.Abs(distance.y) + MathF.Abs(distance.z)) / 2);
    }

    public static List<Hexagon> FindPath(Hexagon startPosition, Hexagon endPosition)
    {
        List<Hexagon> openSet = new List<Hexagon>();
        List<Hexagon> closedSet = new List<Hexagon>();
        openSet.Add(startPosition);

        foreach (GameObject hex in TilesetMap.Values)
        {
            Hexagon hexagon = hex.GetComponent<Hexagon>();
            hexagon.gCost = terrainModifiers[(int)hexagon._terrainType];
            hexagon.hCost = GetDistance(hexagon.hexPosition, endPosition.hexPosition);
            hexagon.fCost = hexagon.gCost + hexagon.hCost;
        }

        while (openSet.Count > 0)
        {
            Hexagon currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                    

                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
    
            if (currentNode == endPosition)
            {
                bool loopFlag = true;
                do
                {
                    loopFlag = false;
                    for (int i = 1; i < closedSet.Count - 1; i++)
                    {
                     List<GameObject> adjacentTiles = HexGrid.GetAdjacentTiles(closedSet[i].hexPosition);
                     if (adjacentTiles.Contains(closedSet[i].gameObject) && closedSet[i].hCost > closedSet[i + 1].hCost)
                     {
                         closedSet[i].DisableHighLight();
                         closedSet.Remove(closedSet[i]);
                         Debug.Log($"Path List {closedSet[i]} removed");
                         
                            loopFlag = true;
                     }
                     
                    }
                } while (loopFlag);
                
                return closedSet;
            }
            
            foreach (GameObject neighbour in HexGrid.GetAdjacentTiles(currentNode.hexPosition))
            {
                Hexagon neighbourHex = neighbour.GetComponent<Hexagon>();
                if (closedSet.Contains(neighbourHex))
                {
                    continue;
                }
              
                
                float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode.hexPosition, neighbourHex.hexPosition);
                if (newMovementCostToNeighbour < neighbourHex.gCost || !openSet.Contains(neighbourHex))
                {
                    neighbourHex.gCost = newMovementCostToNeighbour;
                    neighbourHex.hCost = GetDistance(neighbourHex.hexPosition, endPosition.hexPosition);

                    if (!openSet.Contains(neighbourHex))
                    {
                        openSet.Add(neighbourHex);
                        
                    }
                }
            }
        }

        
        return null;
    }
}