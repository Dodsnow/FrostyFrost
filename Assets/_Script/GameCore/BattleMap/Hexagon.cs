using System.Collections.Generic;
using _Script.PlayableCharacters;
using UnityEngine;

[SelectionBase]
public class Hexagon : MonoBehaviour
{
    [SerializeField] private GlowHighLight _highLight;
    public Vector3Int hexPosition;
    public float fCost;
    public float gCost;
    public float hCost;
    public GameObject _tileObject;
    public TerrainType _terrainType;
    public bool isOccupied = false;
    public ICharacter characterOnHex = null;

    

    private void Awake()
    {
        _highLight = GetComponent<GlowHighLight>();
    }

    public void EnableHighLight()
    {
        
        _highLight.ToggleGlow(true);
    }

    public void EnableExtraHighlight()
    {
        _highLight.ToggleExtraGlow();
    }
    public void DisableHighLight()
    {
        
        _highLight.ToggleGlow(false);
    }

    
}