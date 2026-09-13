using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUnpassedUI : MonoBehaviour
{
    private CanvasGroup selfCanvasGroup;

    private void Awake()
    {
        selfCanvasGroup = GetComponent<CanvasGroup>();
    }
    public void ShowUI()
    {
        selfCanvasGroup.interactable = true;   
        selfCanvasGroup.blocksRaycasts = true;
        selfCanvasGroup.alpha = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AssetsLoader.playerMouseAgent.Enable = false;
    }

    public void CloseUI()
    {
        selfCanvasGroup.interactable = false;
        selfCanvasGroup.blocksRaycasts = false;
        selfCanvasGroup.alpha = 0;
        Cursor.lockState = CursorLockMode.Locked;
        AssetsLoader.playerMouseAgent.Enable = true;
    }
}
