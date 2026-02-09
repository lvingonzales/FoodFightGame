using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    private Vector3 mousePosition;

    private void Start() {
       Cursor.visible = false; 
    }
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3 (mouseWorldPosition.x, mouseWorldPosition.y, 10);
    }
}
