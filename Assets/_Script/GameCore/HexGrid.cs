using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector3Int, GameObject> HexagonTilesetMap = new Dictionary<Vector3Int, GameObject>();
    private List<GameObject> currentSelectedHexes = new List<GameObject>();
    public GameObject hexPrefab;
    private Vector3 hexStartPosition;
    public GameObject obstaclePrefab;


    private void Start()
    {
        AstarPathfinding.HexGrid = this;
        CreateGrid(new Vector2Int(10, 10));
    }

    void CreateGrid(Vector2Int gridSize)
    {
        int row = 0;
        int init_q = 0 - gridSize.y / 2;
        int r_offset = 0;
        int odd_mod_r_counter = 0;

        for (int x = 0; x < gridSize.x; x++)
        {
            r_offset = 0;
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int endPosition = new Vector2Int(x, y);
                if (y % 2 == 1)
                {
                    endPosition.x += 1;
                }
                Random random = new Random();
                GameObject tile = Instantiate(hexPrefab, new Vector3((hexStartPosition.x + endPosition.x + row), 0, hexStartPosition.y + (endPosition.y * 1.73f)), Quaternion.identity);
                Hexagon hex = tile.GetComponent<Hexagon>();
                if (random.Next(0, 100) <= 10)
                {
                    hex._terrainType = TerrainType.Obstacle;
                    hex._tileObject = Instantiate(obstaclePrefab, tile.transform.Find("Props"));
                    
                }
                else
                {
                    hex._terrainType = TerrainType.Normal;
                }

                hex.hexPosition = new Vector3Int(x + r_offset, (x + r_offset + init_q + y) * -1, init_q + y);
                HexagonTilesetMap.Add(hex.hexPosition, tile);

                odd_mod_r_counter++;

                if (odd_mod_r_counter >= 2)
                {
                    r_offset--;
                    odd_mod_r_counter = 0;
                }

                // Debug.Log("=============");
                // Debug.Log("<" + $"{hex.hexPosition.x} {hex.hexPosition.y} {hex.hexPosition.z}" + "> - checksum is " + (hex.hexPosition.x + hex.hexPosition.y + hex.hexPosition.z).ToString());;
                // tile.gameObject.name = $"Hexagon {hex.hexPosition.x} {hex.hexPosition.y} {hex.hexPosition.z}";
            }

            row++;
        }

        AstarPathfinding.TilesetMap = HexagonTilesetMap;
    }

    public List<GameObject> GetAdjacentTiles(Vector3Int position)
    {
        currentSelectedHexes.Clear();

        for (int i = 0; i < 6; i++)
        {
            GameObject hex;
            HexagonTilesetMap.TryGetValue(position + HexDirection.directionList[i], out hex);
            if (hex)
            {
                currentSelectedHexes.Add(hex);
            }
        }

        return currentSelectedHexes;
    }

    enum HexDirectionType
    {
        UP,
        DOWN,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT
    }

    public static class HexDirection
    {
        public static List<Vector3Int> directionList = new List<Vector3Int>()
        {
            new Vector3Int(-1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(0, 1, -1),
            new Vector3Int(-1, 0, 1),
            new Vector3Int(1, 0, -1),
            new Vector3Int(0, -1, 1),
        };
    }
}