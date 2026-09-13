using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject displayFramePrefab;
    public List<DisplayFrame> productFrameList;
    
    public GameObject content;

    public void Start()
    {
        for(int i = 0; i < AssetsLoader.products.Count; i ++)
        {
            var obj = Instantiate<GameObject>(displayFramePrefab, content.transform);
            var displayComponent = obj.GetComponent<DisplayFrame>();
            displayComponent.SetProduct(AssetsLoader.products[i]);
            productFrameList.Add(displayComponent);
        }
    }

    
}
