using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeWorkbench : MonoBehaviour, IWorkbench<IRawMaterial>
{
    private MakeCoffee _makeCoffee;
    private string coffeeProuctTreeSavePath =
#if UNITY_EDITOR
        "D:/info/treeInfo.txt";
#else
        System.IO.Path.Combine(Application.streamingAssetsPath, "treeInfo.txt");
#endif
    private Queue<RawMaterial> _rawMaterials = new Queue<RawMaterial>();

    public int _coffeeWorkbenchID;
    public int GetWorkbenchID => _coffeeWorkbenchID;

    private void GetRawMaterial(RawMaterial rawMaterial)
    {
        _rawMaterials.Enqueue(rawMaterial);
    }

    public void MixRawMaterial()
    {
        while (_rawMaterials.Count > 0)
            _makeCoffee.AddMaterial(_rawMaterials.Dequeue());
    }

    public IProduct Output()
    {
        return _makeCoffee.MakeOver();
    }

    public void Awake()
    {
        _makeCoffee = new MakeCoffee(coffeeProuctTreeSavePath);
        AssetsLoader.workbenchs.Add(GetWorkbenchID,this );
    }

    public void GetRawMaterial(IRawMaterial rawMaterial)
    {
        GetRawMaterial(rawMaterial as RawMaterial);
    }

    public void StartDo()
    {
        _makeCoffee.StartMake();
    }
}
