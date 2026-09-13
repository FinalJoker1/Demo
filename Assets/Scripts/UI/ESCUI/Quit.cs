using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private Button _btn;
    public static Action SerializationAction { set; get; }
    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(GameOver);
    }

    private void GameOver()
    {
        SerializationAction?.Invoke();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
