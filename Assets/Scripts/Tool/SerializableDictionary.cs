using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<T,V> : ISerializationCallbackReceiver,IEnumerable<T>
{
    [SerializeField] private List<T> _serializedKeyList = new List<T>();
    [SerializeField] private List<V> _serializedValueList = new List<V>();

    [NonSerialized] private Dictionary<T,V> RuntimeDict;

    public void OnAfterDeserialize()
    {
        RuntimeDict = new Dictionary<T, V>();
        for(int i = 0; i < _serializedKeyList.Count; i ++)
            RuntimeDict.Add(_serializedKeyList[i], _serializedValueList[i]);
    }

    public void OnBeforeSerialize()
    {
        _serializedKeyList.Clear();
        _serializedKeyList.AddRange(RuntimeDict.Keys);

        _serializedValueList.Clear();
        _serializedValueList.AddRange(RuntimeDict.Values);
    }

    public bool Add(T key,V value) => RuntimeDict.TryAdd(key,value);
    public bool ContainsKey(T key) => RuntimeDict.ContainsKey(key);
    public bool Remove(T key) => RuntimeDict.Remove(key);
    public void Clear() => RuntimeDict.Clear();
    public int Count => RuntimeDict.Count;

    public Dictionary<T,V> GetRealDict() => RuntimeDict;

    public SerializableDictionary()
    {
        RuntimeDict = new Dictionary<T, V>();
    }

    public SerializableDictionary(IEqualityComparer<T> cmp)
    {
        RuntimeDict = new Dictionary<T, V>(cmp);
    }

    public SerializableDictionary(SerializableDictionary<T,V> others)
    {
        RuntimeDict = new Dictionary<T, V>(others.GetRealDict());
    }

    public V this[T t]
    {
        get
        {
            return RuntimeDict[t];
        }
        set
        {
            RuntimeDict[t] = value;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach(var item in RuntimeDict)
            sb.Append(string.Format("key:{0}\tvalue:{1}",item.Key,item.Value));

        return sb.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach(var item in RuntimeDict)
        {
            yield return item.Key;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
