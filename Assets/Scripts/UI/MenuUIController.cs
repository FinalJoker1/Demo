using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    private bool isOpen = false;
    private CanvasGroup canvasGroup;
    private MouseAgent _playerMouseAgent;
    public void Awake()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
        isOpen = canvasGroup.interactable;

    }

    public void Start()
    {
        
        _playerMouseAgent = AssetsLoader.player.GetComponent<MouseAgent>();
        Close();
        _playerMouseAgent.Enable &= !isOpen;
        
    }

    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Show();

            }
        }
        
    }
    public void Update()
    {
        Check();
    }

    public void Show()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isOpen = true;
        _playerMouseAgent.Enable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        isOpen = false;
        _playerMouseAgent.Enable = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
