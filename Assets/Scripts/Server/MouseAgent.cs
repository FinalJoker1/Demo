using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAgent : MonoBehaviour
{
    private bool _enable = true;
    public bool Enable { get => _enable; set => _enable = value; }
    private Vector3 _currentMousePosition;
    public Vector2 GetMouseDisplacement()
    {
        if (Enable)
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        return Vector2.zero;
    }

    public bool GetMouseButtonDown(int index)
    {
        if (Enable)
            return Input.GetMouseButton(index);

        return false;
    }

    public Vector3 mousePosition
    { get 
        { 
            if (Enable) _currentMousePosition = Input.mousePosition; 
            return _currentMousePosition;
        } 
    }
}
