using System.Collections.Generic;

namespace _Script.GameCore.BattleMap.DungeonGeneration
{
    public static class DungeonFactory
    {
        public static List<Dungeon> dungeons = new List<Dungeon>();

        static DungeonFactory()
        {
            Dungeon newDungeon = CreateDungeon("Test Dungeon", 1);
            DungeonRoom newRoom = newDungeon.CreateARoom("Room 1", Enum.RoomShape.Square, Enum.RoomType.NormalFight);
            DungeonRoom newRoom1 = newDungeon.CreateARoom("Room 2", Enum.RoomShape.Circle, Enum.RoomType.NormalFight);
            newRoom.ConnectRoom(newRoom1);
            
        }
        
        public static Dungeon CreateDungeon(string dungeonName, int dungeonDifficulty)
        {
            Dungeon dungeon = new Dungeon(dungeonName, dungeonDifficulty);
            dungeons.Add(dungeon);
            return dungeon;
        }
    }
}