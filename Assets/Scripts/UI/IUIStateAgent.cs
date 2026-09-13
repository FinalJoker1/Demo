using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIStateAgent
{
    void CloseUIInteration();

    void OpenUIInteration();

    void HideUI();

    void ShowUI();
}
