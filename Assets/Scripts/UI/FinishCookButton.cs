using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishCookButton : MonoBehaviour
{
    private Button btn;
    private OrderListUIAgentMono orderListUIAgentMono;
    private ChooseFrame chooseFrame;
    private void Awake()
    {
        btn = GetComponent<Button>();
        orderListUIAgentMono = transform.parent?.GetComponentInChildren<OrderListUIAgentMono>();
        chooseFrame = transform.parent?.GetComponentInChildren<ChooseFrame>();
    }
    void Start()
    {
        btn.onClick.AddListener(AssetsLoader.player.Finish);
        AssetsLoader.trader.AddCallBack(orderListUIAgentMono.OrderListUIAgent.OpenUIInteration);
        AssetsLoader.trader.AddCallBack(() => { chooseFrame.SelfImage.sprite = AssetsLoader.products.DefaultValue.icon; });
    }
}
