using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ts : MonoBehaviour
{
    private RectTransform rt;
    private RectTransform parentRt;

    public void Awake()
    {
        rt = GetComponent<RectTransform>();
        parentRt = transform.parent.GetComponent<RectTransform>();
    }

    public void Update()
    {
        print("current:" + rt.rect.width);
        print("parent:"+ parentRt.rect.width);
    }
}
