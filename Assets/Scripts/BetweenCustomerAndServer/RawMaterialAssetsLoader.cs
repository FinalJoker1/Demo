using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialAssetsLoader : MonoBehaviour
{
    
    public void Start()
    {
        var loadArr = GetComponents<Loader>();

        foreach (var component in loadArr)
            component.Load();
    }
    
}
public interface Loader
{
    void Load();
}
