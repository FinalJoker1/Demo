using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialMono : MonoBehaviour,Loader
{
    [SerializeField] public IRawMaterial Material { get; private set; }
    public int rawMaterialID;

    public void Load()
    {
        RawMaterial material;
        AssetsLoader.rawMaterials.TryGetValue(rawMaterialID, out material);

        Material = material;
    }
}
