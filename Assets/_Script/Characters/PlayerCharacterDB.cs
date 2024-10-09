using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

[CreateAssetMenu(fileName = "PlayerCharacterDB", menuName = "ScriptableObjects/PlayerCharacterDB", order = 0)]

    public class PlayerCharacterDB : ScriptableObject
    {
        public  List<PlayerCharacterTemplate> playerCharacters = new List<PlayerCharacterTemplate>();
    }

[Serializable]
    public class PlayerCharacterTemplate
    {
        public int characterID;
        public string characterName;
        public int maxHealth;
        public int handSize;
        public GameObject characterPrefab;
        public Sprite characterIcon;
        public GameObject characterCardPrefab;
        public ClassType classType;

        public Dictionary<ClassType, List<CharacterCard>> characterCards =
            new Dictionary<ClassType, List<CharacterCard>>();
    }
