using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkbench<T> where T : IRawMaterial 
{
    void StartDo();
    void GetRawMaterial(T rawMaterial);

    void MixRawMaterial();

    IProduct Output();

    int GetWorkbenchID { get; }
}

public interface IRawMaterial
{
    int RawMaterialID { get; }
}
