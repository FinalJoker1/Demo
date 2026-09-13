using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void Awake()
    {
        AssetsLoader.timeManager = this;
    }
    public IEnumerator Timer(float seconds,Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }
}
