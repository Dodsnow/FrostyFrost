using _Script.GameCore.BattleMap.DungeonGeneration;
using UnityEngine;

namespace _Script.GameCore.City.Buttons
{
    public class Descend : MonoBehaviour
    {
        public void DescendOnClick()
        {
            DungeonGeneration dungeonGenerator = new DungeonGeneration(DungeonFactory.dungeons[0]);
            dungeonGenerator.GenerateDungeon();
        }
    }
}