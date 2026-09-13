using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultList<T> where T : class
{ 
    public T DefaultValue { get; private set; }
    public List<T> GetList { get; private set; }

    public int Count => GetList.Count;
    public void Add(T value) => GetList.Add(value);
    public void Clear() => GetList.Clear();

    public bool Remove(T value) => GetList.Remove(value);

    public void RemoveAt(int i) => GetList.RemoveAt(i);

    public DefaultList(T defaultValue = null)
    {
        DefaultValue = defaultValue;
        GetList = new List<T>();
    }

    public DefaultList(List<T> list, T defaultValue)
    {
        DefaultValue = defaultValue;
        GetList = list;
    }

    public DefaultList(IEnumerable<T> collection,T defaultValue)
    {
        DefaultValue = defaultValue;
        GetList = new List<T>(collection);
    }

    public void SetDefalueValue(T value) => DefaultValue = value;
    public bool IsDefault(T value)
    {
        return DefaultValue.Equals(value);
    }
    public T this[int i]
    {
        get => GetList[i];
        set => GetList[i] = value;
    }
}
