    using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MouseSimple : MonoBehaviour
{
    public UnityEvent<Vector3> PointerClick;


    void Update()
    {
        DetectMouseClick();
    }

    private void DetectMouseClick()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
           
            Vector3 mousePos = Input.mousePosition;
            PointerClick?.Invoke(mousePos);
        }
    }
}