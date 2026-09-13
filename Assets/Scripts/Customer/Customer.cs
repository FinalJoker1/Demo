using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Customer",        // 默认新建出来的文件名
    menuName = "MyGame/Customer", // 右键菜单路径
    order = 1
)]
public class Customer : ScriptableObject,IEquatable<Customer>
{
    public string name;
    public int id;
    public Table table;
    public Ordering orderingInfo;

    public bool Equals(Customer other)
    {
        return id == other.id;
    }

    public override int GetHashCode()
    {
        return id;
    }
}
