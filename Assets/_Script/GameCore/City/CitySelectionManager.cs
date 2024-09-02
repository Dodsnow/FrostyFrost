using UnityEngine;

namespace _Script.GameCore.City.Buildings
{
    public class CitySelectionManager : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject building = hit.collider.gameObject;
                    Debug.Log(building.name);
                    GameBuilding gameBuilding = building.GetComponent<GameBuilding>();
                    gameBuilding.OnClick();
                }
            }
        }
    }
}