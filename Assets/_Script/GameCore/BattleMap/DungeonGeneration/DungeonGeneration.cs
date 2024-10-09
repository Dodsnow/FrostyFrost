using System.Threading.Tasks;
using _Script.GameCore.BattleMap.DungeonGeneration.Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Script.GameCore.BattleMap.DungeonGeneration
{
    public class DungeonGeneration
    {
        private RoomShape _roomShape;
        
        
        
        
        public DungeonGeneration(Dungeon dungeon)
        {
            SceneManager.LoadScene("BattleMap", LoadSceneMode.Single);
            var t = Task.Run(async delegate
            {
                await Task.Delay(1000);
                HexGridReference.HexGrid.CreateGrid(new Vector2Int(10, 10));
                BattleManagerReference.BattleManager.StartBattle();


            });
        }

      
        

        public void GenerateDungeon()
        {
            switch (_roomShape)
            {
                case RoomShape.Square:
                    GenerateRectangleRoom();
                    break;
                case RoomShape.Circle:
                    GenerateCircleRoom();
                    break;
                case RoomShape.Hexagon:
                    GenerateHexagonRoom();
                    break;
                case RoomShape.Triangle:
                    GenerateTriangleRoom();
                    break;
            }
        }
        
        private void GenerateRectangleRoom()
        {
            // Generate a rectangle room
            
        }
        
        private void GenerateCircleRoom()
        {
            // Generate a circle room
        }
        
        private void GenerateHexagonRoom()
        {
            // Generate a hexagon room
        }
        
        private void GenerateTriangleRoom()
        {
            // Generate a triangle room
        }
    }
}