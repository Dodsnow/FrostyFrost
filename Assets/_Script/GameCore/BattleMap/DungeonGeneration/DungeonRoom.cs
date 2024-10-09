using System.Collections.Generic;
using System.Diagnostics;
using _Script.GameCore.BattleMap.DungeonGeneration.Enum;
using NUnit.Framework;

namespace _Script.GameCore.BattleMap.DungeonGeneration
{
    public class DungeonRoom
    {
        private string _roomName;
        private RoomShape _roomShape;
        private RoomType _roomType;
        private List<DungeonRoom> _availableRooms = new List<DungeonRoom>();
        private bool _lastRoom = false;
        
        public DungeonRoom(string roomName,  RoomShape roomShape, RoomType roomType)
        {
            _roomName = roomName;
            _roomShape = roomShape;
            _roomType = roomType;
        } 
        
        public void ConnectRoom(DungeonRoom room)
        {
            _availableRooms.Add(room);
            
        }

        public void CreateARoom()
        {
            
        }
    }
}