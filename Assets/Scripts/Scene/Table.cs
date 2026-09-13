using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Table",        // 默认新建出来的文件名
    menuName = "MyGame/Table", // 右键菜单路径
    order = 1
)]
public class Table : ScriptableObject
{
    public string name;
    public int maxCapacity;
    public int remainingCapacity;
}
