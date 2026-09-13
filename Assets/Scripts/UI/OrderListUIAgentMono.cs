using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUIAgentMono : MonoBehaviour
{
    public float noninterationAlphaValue = 0.5f;
    public float interationAlphaValue = 1.0f;
    public OrderListUIAgent OrderListUIAgent { get; private set; }

    public void Awake()
    {
        OrderListUIAgent = new OrderListUIAgent(GetComponent<CanvasGroup>(), noninterationAlphaValue, interationAlphaValue);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(OrderListUIAgent.cursorIsLocked)
            {
                OrderListUIAgent.ShowCursor();
            }
            else
            {
                OrderListUIAgent.CloseCursor();
            }    
        }
    }
}
