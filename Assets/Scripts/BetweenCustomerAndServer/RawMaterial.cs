using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "RawMaterial",        // 默认新建出来的文件名
    menuName = "MyGame/RawMaterial", // 右键菜单路径
    order = 1
)]
[System.Serializable]
public class RawMaterial : ScriptableObject ,IEquatable<RawMaterial>,IRawMaterial
{
    [SerializeField] public int ID;

    [SerializeField] public string description;
    [JsonIgnore] public int RawMaterialID => this.ID;

    public bool Equals(RawMaterial other)
    {
        return this.ID == other.ID;
    }

    public override int GetHashCode()
    {
        return ID;
    }

}
