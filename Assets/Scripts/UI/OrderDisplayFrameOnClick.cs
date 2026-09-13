using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplayFrameOnClick : MonoBehaviour
{
    private Button btn;
    private static PlayerAgent player;
    public string chooseFrameName = "ChooseFrame";
    private Image chooseFrameImage;
    public string orderListName = "OrderList";
    private OrderListUIAgentMono orderListUIAgent;
    public string UITag = "UI";
    private OrderDisplayFrame selfFrame;
    public OrderDisplayFrame SelfFrame 
    { get
        {
            if (selfFrame == null)
                selfFrame = GetComponent<OrderDisplayFrameMono>()?.selfFrame;
            return selfFrame;
        }
    }
    public void Awake()
    {
        chooseFrameImage = GameObject.FindWithTag(UITag).GetComponentInChildren<ChooseFrame>()?.GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(StartMake);
        btn.onClick.AddListener(FillChooseFrame);
        btn.onClick.AddListener(CloseOrderListUIInteration);
        //btn.onClick.AddListener(Test);
        orderListUIAgent = transform.GetComponentInParent<OrderListUIAgentMono>();
        
    }

    public void Start()
    {
        player = AssetsLoader.player;
    }

    public void StartMake()
    {
        if(player != null &&  SelfFrame != null)
        {
            player.StartMake(SelfFrame);
        }
    }

    public void CloseOrderListUIInteration()
    {
        if (orderListUIAgent == null)
            return;

        orderListUIAgent.OrderListUIAgent.CloseUIInteration();
    }

    public void FillChooseFrame()
    {
        if(selfFrame != null)
            chooseFrameImage.sprite = selfFrame.iconShow.sprite;
    }

    public void Test()
    {
        print("chooseOrder:" + selfFrame.product.id);
    }
}
