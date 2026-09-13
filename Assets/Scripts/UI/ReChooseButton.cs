using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReChooseButton : MonoBehaviour
{
    private Button btn;
    private OrderListUIAgentMono orderListUIAgentMono;
    private ChooseFrame chooseFrame;

    public void Awake()
    {
        btn = GetComponent<Button>();
        orderListUIAgentMono = transform.parent?.GetComponentInChildren<OrderListUIAgentMono>();
        chooseFrame = transform.parent?.GetComponentInChildren<ChooseFrame>();
    }

    public void Start()
    {
        btn?.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        AssetsLoader.player.StopMake();
        chooseFrame.SelfImage.sprite = AssetsLoader.products?.DefaultValue?.icon;
        orderListUIAgentMono.OrderListUIAgent.OpenUIInteration();
    }
}
