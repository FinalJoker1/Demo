using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Trader : MonoBehaviour
{
    public void Awake()
    {
        AssetsLoader.trader = this;
    }

    private Action taskCallBack;
    public void AddCallBack(Action listener) => taskCallBack += listener;
    public void RemoveCallBack(Action listtener) => taskCallBack -= listtener;
    private HashSet<ProductTrade<ISeller, IBuyer>> productTrades = new HashSet<ProductTrade<ISeller, IBuyer>>();
    public HashSet<ProductTrade<ISeller, IBuyer>> TradeChannels { get => productTrades;}
    public void StartTrade(ISeller seller,IBuyer buyer,Action successCallBack = null,Action failedCallBack = null)
    {
        var trade = new ProductTrade<ISeller,IBuyer>(seller, buyer);
        TradeChannels.Add(trade);

        //开子线程执行交易的具体实现，最后把回调函数投递到主线程中执行
        Task<bool>.Run(() => trade.Trade((param) => TradeChannels.Remove(param as ProductTrade<ISeller, IBuyer>))).
                   ContinueWith(task => { AssetsLoader.mainThreadDispatcher.Post(taskCallBack);if (task.Result) AssetsLoader.mainThreadDispatcher.Post(successCallBack); else AssetsLoader.mainThreadDispatcher.Post(failedCallBack); });

    }

    
}
