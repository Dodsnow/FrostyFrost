using System.Collections.Generic;
using NUnit.Framework;


namespace _Script.GameCore.BattleMap.DungeonGeneration
{
    public class Dungeon
    {
        private string _dungeonName;
        private int _dungeonDifficulty;
        private List<DungeonRoom> _rooms = new List<DungeonRoom>();
        private int _currentRoom;
        
        public Dungeon(string dungeonName, int dungeonDifficulty)
        {
            _dungeonName = dungeonName;
            _dungeonDifficulty = dungeonDifficulty;
        }
        
        public DungeonRoom CreateARoom(string roomName, Enum.RoomShape roomShape, Enum.RoomType roomType)
        {
            DungeonRoom newRoom = new DungeonRoom(roomName, roomShape, roomType);
            _rooms.Add(newRoom);
            return newRoom;
        }
    }
}