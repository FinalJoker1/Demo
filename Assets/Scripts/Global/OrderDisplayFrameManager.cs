using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplayFrameManager : MonoBehaviour
{
    public GameObject orderDisplayFramePrefab;
    public SerializableHashSet<OrderDisplayFrame> orderDisplayFrames = new SerializableHashSet<OrderDisplayFrame>();
    private FrameBuilder frameBuilder = new FrameBuilder();
    

    public void Awake()
    {
        AssetsLoader.orderDisplayFrameManager = this;
    }
    public OrderDisplayFrame Add(Product product,int count,IBuyer buyer)
    {
        var prefab = GameObject.Instantiate<GameObject>(orderDisplayFramePrefab,this.transform);
        var orderDisplayFrameMono = prefab.GetComponent<OrderDisplayFrameMono>();
        var res = AddCommon(prefab, product, count, orderDisplayFrameMono, buyer);
        orderDisplayFrameMono.selfFrame = res;
        orderDisplayFrames.Add(res);
        return res;
    }

    private OrderDisplayFrame AddCommon(GameObject prefab,Product product, int count,OrderDisplayFrameMono orderDisplayFrameMono,IBuyer buyer)
    {
        return  frameBuilder.Begain(prefab).
                GetImage(orderDisplayFrameMono.Icon).
                GetTextMeshProUGUI(orderDisplayFrameMono.TXT).
                GetProduct(product, count).
                GetBuyer(buyer).
                Build();
    }

    public void Romove(OrderDisplayFrame orderDisplayFrame,GameObject frameGO)
    {
        orderDisplayFrames.Remove(orderDisplayFrame);
        Destroy(frameGO);
    }
}
