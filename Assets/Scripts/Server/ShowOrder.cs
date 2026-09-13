using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOrder : MonoBehaviour
{
    private Coroutine handleCoroutine;
    private Coroutine checkCoroutine;
    public int id;

    public void Awake()
    {
        id = 1;
        AssetsLoader.showOrder.Add(id, this);
    }

    //public void EnableServer()
    //{
    //    checkCoroutine = StartCoroutine(Check());
    //}

    public void DisableServer()
    {
        if(handleCoroutine != null)
            StopCoroutine(handleCoroutine);

        if (checkCoroutine != null)
            StopCoroutine(checkCoroutine);

        handleCoroutine = null;
        checkCoroutine = null;
    }

    public void GetMsg(Message msg)
    {
        //通过传入的Message对象初始化订单信息并加载到订单列表里
        foreach (var item in msg.orderingList)
        {
            var frame = AssetsLoader.orderDisplayFrameManager.Add(item, msg.orderingList[item],msg.Buyer);
            frame.Load();
        }
    }
}
