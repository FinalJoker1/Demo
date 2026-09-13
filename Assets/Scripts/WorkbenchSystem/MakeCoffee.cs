using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoffee 
{
    private ProductBuilder pb;
    private string _treeSavePath;

    public MakeCoffee(string path)
    {
        _treeSavePath = path;
        pb = new ProductBuilder(path);
    }
    public void StartMake()
    {
        pb.Begin();
    }
    public void AddMaterial(IRawMaterial material)
    {
        if (pb == null || material == null)
            return;

        pb.Add(material.RawMaterialID);
    }

    public Product MakeOver()
    {
        Product res = pb.Build();
        pb.Reset();
        return res;
    }

    
}
