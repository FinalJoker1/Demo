using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Client : IBuyer
{
    public enum State
    {
        Ordering,
        Waiting,
        Done
    }
    private Customer customer;
    private OrderingBuilder ob;
    private ShowOrder showOrder;
    public Client.State state;
    private SerializableDictionary<Product, int> orderingCopy;
    private Menu menu;
    public Action<object> OrderDone;
    public Message msgCopy { get; private set; }

    public Action Unpassed => null;

    public Client(Customer customer, ShowOrder showOrder, Menu menu)
    {
        this.customer = customer;
        this.ob = new OrderingBuilder();
        this.showOrder = showOrder;
        this.state = Client.State.Done;
        this.menu = menu;
    }

    public void Add(Product product)
    {
        ob.AddFood(product);
        state = State.Ordering;
    }

    public void Remove(Product product)
    {
        ob.RemoveFood(product);
    }

    public void Submit()
    {
        customer.orderingInfo = ob.Build();
        orderingCopy = new SerializableDictionary<Product, int>(customer.orderingInfo.orderingDict.GetRealDict().Comparer);
        foreach (var item in customer.orderingInfo.orderingDict)
            orderingCopy.Add(item, customer.orderingInfo.orderingDict[item]);
       
        msgCopy = new Message(this, orderingCopy);
        state = State.Waiting;
        showOrder.GetMsg(msgCopy);
        //Debug.Log(string.Format("customer id:{0}\norder list:{1}",customer.id,orderingCopy));
    }

    private bool GetFromServer(Product product)
    {
        if (orderingCopy.ContainsKey(product) == false)
        {
            Unpassed?.Invoke();
            return false;
        }
            

        orderingCopy[product]--;

        if (orderingCopy[product] <= 0)
            orderingCopy.Remove(product);

        if (orderingCopy.Count <= 0)
        {
            state = State.Done;
            OrderDone?.Invoke(this);
            msgCopy = null;
        }
        
        return true;
    }

    public bool Check(IProduct product)
    {
        return GetFromServer(product as Product);
    }

    public ICurrency Pay(ICurrency require)
    {
        return require;
    }
}
