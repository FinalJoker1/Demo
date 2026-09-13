using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    public float threadCallBackActionDetectionIntervalTime = 0.02f;

    private List<Action> _callBackList = new List<Action>();
    public void Dispatcher(Action callback) => _callBackList.Add(callback);
    private readonly object _lock = new object();

    public void Awake()
    {
        AssetsLoader.mainThreadDispatcher = this;
    }

    public void Start()
    {
        StartCoroutine(Interval());
    }

    public void Post(Action action)
    {
        _callBackList.Add(action);
    }
    private IEnumerator Interval()
    {
        float endTime = Time.time;

        while (true)
        {
            Action action = null;
            lock(_lock)
            {
                if(_callBackList.Count > 0)
                {
                    action = _callBackList[0];
                    _callBackList.RemoveAt(0);
                }            
            }
            action?.Invoke();

            endTime = Time.time + threadCallBackActionDetectionIntervalTime;
            while (Time.time < endTime)
                yield return null;

            
        }
    }
}
