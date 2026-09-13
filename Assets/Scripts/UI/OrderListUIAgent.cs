using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListUIAgent : IUIStateAgent
{
    private CanvasGroup canvasGroup;
    public float noninterationAlphaValue = 0.5f;
    public float interationAlphaValue = 1.0f;
    private bool interatable = false;
    public bool cursorIsLocked { get => AssetsLoader.playerMouseAgent.Enable; }

    public OrderListUIAgent(CanvasGroup canvasGroup,float noninterationAlphaValue = 0.5f,float interationAlphaValue = 1.0f)
    {
        this.canvasGroup = canvasGroup;
    }

    public void CloseUIInteration()
    {
        if (canvasGroup == null)
            return;

        canvasGroup.interactable = this.interatable = false;
        canvasGroup.alpha = noninterationAlphaValue;
    }

    public void HideUI()
    {
        if (canvasGroup == null)
            return;

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OpenUIInteration()
    {
        if (canvasGroup == null)
            return;

        canvasGroup.interactable = this.interatable = true;
        canvasGroup.alpha = interationAlphaValue;
    }

    public void ShowUI()
    {
        if (canvasGroup == null)
            return ;

        canvasGroup.interactable = this.interatable;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = canvasGroup.interactable ? interationAlphaValue : noninterationAlphaValue;
    }

    public void ShowCursor()
    {
        AssetsLoader.playerMouseAgent.Enable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseCursor()
    {
        AssetsLoader.playerMouseAgent.Enable = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
