using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTrade<S, B> : ITradingSystem<S, B> where S : ISeller where B : IBuyer
{
    private S seller;
    public S Seller => seller;

    private B buyer;
    public B Buyer => buyer;

    private Action<object> EndCallBack;

    public ProductTrade(S seller, B buyer)
    {
        this.seller = seller;
        this.buyer = buyer;
    }
    public bool Trade(Action<object> EndCallBack)
    {
        this.EndCallBack = EndCallBack;
        return StartTrade();
    }

    protected virtual bool StartTrade() => SellerDeliveryProduct();
    protected virtual bool SellerDeliveryProduct()
    {
        return BuyerCheck(Seller.GetProduct());
    }

    protected virtual bool BuyerCheck(IProduct prodcut)
    {
        if (Buyer.Check(prodcut))
        {
            Debug.Log("Buyer check passed");
            return SellerAskForPayment();
        }
        Debug.Log("Buyer check unpassed");
        EndTheDeal(Buyer.Unpassed);
        return false;
    }

    protected virtual bool SellerAskForPayment()
    {
        return SellerCheck(Buyer.Pay(Seller.Need()));
    }

    public virtual bool SellerCheck(ICurrency value)
    {
        if(Seller.Check(value))
        {
            Debug.Log("Seller check passed");
            EndTheDeal(null);
            return true;
        }

        Debug.Log("Seller check unpassed");
        EndTheDeal(Seller.Unpassed);
        return false;
    }

    protected virtual void EndTheDeal(Action EndDo)
    {
        EndDo?.Invoke();
        Destory();
    }

    protected virtual void Destory()
    {
        EndCallBack?.Invoke(this);
    }
}
