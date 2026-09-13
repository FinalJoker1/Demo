using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssetsLoader : MonoBehaviour
{
    //资源加载部分
    public static DefaultList<Product> products;
    public static DefaultList<Customer> customers;
    public static Dictionary<int, RawMaterial> rawMaterials;

    //其它脚本要提供的全局变量
    public static Dictionary<int, IWorkbench<IRawMaterial>> workbenchs = new Dictionary<int, IWorkbench<IRawMaterial>>();
    public static SerializableDictionary<int, ShowOrder> showOrder = new SerializableDictionary<int, ShowOrder>();
    public static OrderDisplayFrameManager orderDisplayFrameManager;
    public static TimeManager timeManager;
    public static PlayerAgent player;
    public static MouseAgent playerMouseAgent;
    public static Trader trader;
    public static MainThreadDispatcher mainThreadDispatcher;
    public static Json jsonManager;

    //随机数种子
    public int randomSeek = 100;
    
    public void CustomersLoader()
    {
        customers = new DefaultList<Customer>( Resources.LoadAll<Customer>("ScriptableObject/Customers"),null);
    }

    public void RawMaterialsLoader()
    {
        rawMaterials = new Dictionary<int, RawMaterial>();
        var rawMaterialsArr = Resources.LoadAll<RawMaterial>("ScriptableObject/RawMaterials");
        foreach(var material in rawMaterialsArr)
        {
            rawMaterials.Add(material.ID, material);
        }
    }

    public void ProductsLoader()
    {
        var arr = Resources.LoadAll<Product>("ScriptableObject/Products");
        products = new DefaultList<Product>(); 
        foreach (var item in arr)
        {
            if (item.id == -1)
                products.SetDefalueValue(item);
            else
                products.Add(item);
        }
    }

    public void Awake()
    {
        CustomersLoader();
        ProductsLoader();
        RawMaterialsLoader();
        Random.InitState(randomSeek);
    }
}
