using System;
using System.IO;
using _Script.GameCore;
using UnityEngine;

namespace _Script.GameCore
{
    
    public static class SaveSystem
    {
       public static GameState _gameState = new();

        // static SaveSystem()
        // {
        //     SaveGame(_gameState);
        // }


        public static void SaveGame<T>(T stringToSave)
        {
            string jsonString = JsonUtility.ToJson(stringToSave);
            File.WriteAllText("Assets/Save/Save.json", jsonString);
            Debug.LogWarning("SaveFiles created");
            Debug.LogWarning(jsonString);
        }


        public static void LoadGame<T>(T stringToLoad)
        {
            string jsonString = File.ReadAllText("Assets/Save/Save.json");
            JsonUtility.FromJsonOverwrite(jsonString, stringToLoad);
            PlayerInventory.Resources[ResourceType.Metal] = _gameState.metal;
            PlayerInventory.Resources[ResourceType.Gold] = _gameState.gold;
            PlayerInventory.Resources[ResourceType.Wood] = _gameState.wood;
            PlayerInventory.Resources[ResourceType.Hide] = _gameState.hide;
            Debug.LogWarning("SaveFiles loaded");
            Debug.LogWarning(jsonString);
        }
       
    }
}