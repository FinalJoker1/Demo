using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 此类实现Buyer是为了Check的时候要额外先判断product是否与传过来的相同
/// </summary>
public class OrderDisplayFrame : IBuyer
{
    public Product product;
    public Image iconShow;
    public TMPro.TextMeshProUGUI txtShow;
    public int count;
    public GameObject frame;
    public IBuyer Buyer { get; set; }

    public Action Unpassed => Buyer.Unpassed;

    public bool Check(IProduct product)
    {
        //不仅要判断传过来的值与缓存在当前组件的product是否相同，还要判断真正
        //对应的顾客订单请求中是否包含此id的Product
        return this.product.Equals(product as Product) && Buyer.Check(product);
    }

    /// <summary>
    /// 加载Product的图片以及数量文字
    /// </summary>
    public void Load()
    {
        iconShow.sprite = product.icon;
        txtShow.text = count.ToString();
    }

    public ICurrency Pay(ICurrency require)
    {
        return Buyer.Pay(require);
    }
}
