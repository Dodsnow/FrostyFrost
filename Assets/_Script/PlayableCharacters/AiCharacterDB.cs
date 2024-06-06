
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "AiCharacterDB", menuName = "ScriptableObjects/AiCharacterDB", order = 1)]

    public class AiCharacterDB : ScriptableObject
    {
        public List<AiCharacterTemplate> aiCharacters = new List<AiCharacterTemplate>();
    }

    [Serializable]
    public class AiCharacterTemplate
    {
        public int characterID;
        public string characterName;
        public int maxHealth;
        public MonsterType monsterType;
        public GameObject characterPrefab;

    }

