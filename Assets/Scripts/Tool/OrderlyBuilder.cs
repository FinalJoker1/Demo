using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 用于得到按某个顺序构建的对象的最终的形态
/// TSELF，Match，Toutput中不要使用字段而是使用属性，如果需要私有化，可以给属性的set添加private修饰同时为属性添加[JsonProperty]特性
/// </summary>
/// <typeparam name="TMatch">构建时每一步所需的原料的类型，这个类型必须实现了接口IEquatable，注意这个接口的实现方法不包含GetHashCode，但是GetHashCode仍需重写，不然可能出错</typeparam>
/// <typeparam name="TOutput">最终的输出形态的类型，必须是一个可实例化的包含一个无参构造函数的类</typeparam>
/// <typeparam name="TSELF">自泛型</typeparam>
public class OrderlyBuilder<TMatch, TOutput, TSELF> where TMatch : IEquatable<TMatch> where TSELF : OrderlyBuilder<TMatch, TOutput, TSELF> where TOutput : class, new()
{
    [JsonProperty] private static Node _tree;
    private Node _point;
    [JsonProperty] public bool Interrupt { get; private set; }
    public class Node
    {
        public TOutput output;

        public Dictionary<TMatch, Node> children = new Dictionary<TMatch, Node>();

    }

    public Node GetTree() => _tree;
    public OrderlyBuilder()
    {

    }
    public OrderlyBuilder(string path)
    {
       LoadTree(path,Json.jsonSettings);
    }

    public OrderlyBuilder(Node tree)
    {
        _tree = tree;
    }

    protected void LoadTree(string path,JsonSerializerSettings settings = null)
    {
        string json = File.ReadAllText(path);
        _tree = JsonConvert.DeserializeObject<Node>(json,settings);
    }

    public TSELF Begin()
    {
        Reset();
        return (TSELF)this;
    }

    public TSELF Add(TMatch value)
    {
        if (Interrupt == false &&  _point != null && _point.children.ContainsKey(value))
        {
            _point = _point.children[value];
        }
        else
        {
            _point = null;
            Interrupt = true;
        }

        return (TSELF)this;
    }

    public TOutput Build()
    {
        return _point?.output;
    }

    public void Reset()
    {
        _point = _tree;
        Interrupt = false;
    }

}





