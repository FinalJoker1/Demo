using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableHashSet<T> : ISerializationCallbackReceiver
{
    // 给Inspector编辑、Unity序列化用的中介List
    [SerializeField] private List<T> _serializedList = new List<T>();

    // 运行时真正使用的HashSet，[NonSerialized]不参与unity序列化
    [NonSerialized] private HashSet<T> RuntimeSet = new HashSet<T>();

    // 反序列化完成后：List数据拷贝进HashSet（Awake之前执行）
    public void OnAfterDeserialize()
    {
        RuntimeSet = new HashSet<T>(_serializedList);
    }

    // 序列化之前：HashSet同步回List（保存场景/预制体时调用）
    public void OnBeforeSerialize()
    {
        _serializedList.Clear();
        _serializedList.AddRange(RuntimeSet);
    }

    // 方便对外暴露常用方法，代理到RuntimeSet
    public bool Add(T item) => RuntimeSet.Add(item);
    public bool Contains(T item) => RuntimeSet.Contains(item);
    public bool Remove(T item) => RuntimeSet.Remove(item);
    public void Clear() => RuntimeSet.Clear();
    public int Count => RuntimeSet.Count;

    public HashSet<T>.Enumerator GetEnumerator() => RuntimeSet.GetEnumerator();
}
