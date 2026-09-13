using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCUI : MonoBehaviour
{
    public bool isOpen = false;
    private CanvasGroup _canvasGroup;
    private MouseAgent _playerMouseAgent;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        isOpen = _canvasGroup.interactable;
    }

    private void Start()
    {

        _playerMouseAgent = AssetsLoader.playerMouseAgent;
        _playerMouseAgent.Enable &= !isOpen;
    }

    public void Open()
    {
        _playerMouseAgent.Enable = false;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
        isOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        _playerMouseAgent.Enable = true;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Check()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
    private void Update()
    {
        Check();
    }
    
}
