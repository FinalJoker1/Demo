using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public float mouseMoveDetectionIntervalTime = 0.02f;
    public float downwardAngle = -80;
    public float elevationAngle = 80;
    public float mouseSensitivity = 120f;
    private float _pitch;
    private Camera _playerCamera;
    private MouseAgent _mouseAgent;

    public void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        
    }

    private void Start()
    {
        _mouseAgent = AssetsLoader.playerMouseAgent ;
    }

    public void Update()
    {
        Vector2 delta = _mouseAgent.GetMouseDisplacement();
        float mouseX = delta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = delta.y * mouseSensitivity * Time.deltaTime;

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, downwardAngle, elevationAngle);

        _playerCamera.transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
