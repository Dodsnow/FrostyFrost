using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public LayerMask selectionMask;

    public HexGrid hexGrid;
    private List<Vector3> _neighbours;
    private List<Hexagon> _lastSelectedHexes = new List<Hexagon>();
    private GameObject _finalHex;
    private GameObject _startHex;

    private void Awake()
    {
        _neighbours = new List<Vector3>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if (FindTarget(mousePosition, out result))
        {
            if (_startHex == null)
            {
                _startHex = result;
                _startHex.GetComponent<Hexagon>().EnableHighLight();
            }
            else if (_finalHex == null)
            {
                _finalHex = result;
                _finalHex.GetComponent<Hexagon>().EnableHighLight();
                
                _lastSelectedHexes = AstarPathfinding.FindPath(_startHex.GetComponent<Hexagon>(), _finalHex.GetComponent<Hexagon>());
                foreach (Hexagon hexes in _lastSelectedHexes)
                {
                    hexes.EnableHighLight();
                }
            }
            else
            {
                if (_lastSelectedHexes != null)
                {
                    foreach (Hexagon hexes in _lastSelectedHexes)
                    {
                        hexes.DisableHighLight();
                    }
                }
               
                _startHex = null;
                _finalHex = null;
            }

            // if (lastSelectedHexes.Count > 0) {
            //     foreach(GameObject hex in lastSelectedHexes){
            //         Hexagon current_hexagon = hex.GetComponent<Hexagon>();
            //         current_hexagon.DisableHighLight();
            //     }
            // }


            // Hexagon hexagon = result.GetComponent<Hexagon>();
            // List<GameObject> hexList = hexGrid.GetAdjacentTiles(hexagon.hexPosition);
            //
            //     foreach(GameObject hex in hexList){
            //         Hexagon current_hexagon = hex.GetComponent<Hexagon>();
            //         current_hexagon.EnableHighLight();
            //     }
            //
            // lastSelectedHexes = hexList;
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}