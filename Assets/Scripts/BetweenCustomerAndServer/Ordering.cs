using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ordering 
{
    public SerializableDictionary<Product,int> orderingDict = new SerializableDictionary<Product,int>(new ProductCompare());
    public int cost;
}

