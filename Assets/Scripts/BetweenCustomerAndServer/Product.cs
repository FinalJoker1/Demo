using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Product",        // 默认新建出来的文件名
    menuName = "MyGame/Product", // 右键菜单路径
    order = 1
)]
public class Product : ScriptableObject,IEquatable<Product>,IProduct
{
    public int id;
    public string name;
    public string description;
    public int price;
    private Vector2 range;
    public Vector2 Range { get => range;set { range.x = Mathf.Min(value.x, value.y);range.y = value.x + value.y - range.x; } }
    [JsonIgnore] // 序列化JSON忽略Sprite引用
    public Sprite icon;
    public int linkWorkbenchID;

    [JsonProperty("iconRef")]
    public SubSpriteRef IconRef
    {
        get
        {
            if (icon == null)
                return null;
            SubSpriteRef iconRef = new SubSpriteRef();
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(icon, out string guid, out long localId);
            iconRef.altasGuid = guid;
            iconRef.localId = localId;
#else
            var nameSplit = icon.name.Split('_');
            iconRef.altasGuid = nameSplit[0];

            if (nameSplit.Length < 2)
                iconRef.localId = null;
            else
                iconRef.localId = Int64.Parse(nameSplit[1]);
#endif
            return iconRef;
        }
        set
        {
            if(value == null || string.IsNullOrEmpty(value.altasGuid))
            {
                icon = null;
                return;
            }
#if UNITY_EDITOR
            icon = SubSpriteRef.GetSprite( value.altasGuid, value.localId.Value);

#else
            icon = ResourcesLoad.Load("Image/" + value.altasGuid, value.localId) as Sprite;
#endif
        }
    }


    object IProduct.Product => this;

    public bool Equals(Product other)
    {
        return id == other?.id;
    }
}

public class ProductCompare : IEqualityComparer<Product>
{
    public bool Equals(Product x, Product y)
    {
        return x.id == y.id;
    }

    public int GetHashCode(Product obj)
    {
        return obj.id;
    }
}


public class ResourcesLoad
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="guid">实际上为资源名字</param>
    /// <param name="localId">如果是图集等含子集的资源，这个是子集元素的id</param>
    /// <returns></returns>
    public static UnityEngine.Object Load(string guid,long? localId)
    {
        if(localId == null)
            return Resources.Load(guid);

        var objs = Resources.LoadAll(guid);
        
        foreach(var obj in objs)
        {
            if(obj.name.Equals(guid + "_" + localId))
                return obj;
        }

        return null;
    }
}
public class SubSpriteRef
{
    public static Sprite GetSprite(string guid, long localId)
    {
#if UNITY_EDITOR
        var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
        UnityEngine.Object[] objs = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);

        foreach (var sp in objs)
        {
            if (sp is Sprite && UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sp, out string spGuid, out long spLoaclId))
            {
                if (guid == spGuid && localId == spLoaclId)
                    return sp as Sprite;
            }
        
        }
#else
#endif
        return null;
    }
    public string altasGuid;
    public long? localId;
}