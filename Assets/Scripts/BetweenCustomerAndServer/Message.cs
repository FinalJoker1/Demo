using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Message : IEquatable<Message>
{
    public SerializableDictionary<Product, int> orderingList { get; private set; }
    public IBuyer Buyer { get; private set; }
    public int id;
     
    public Message(IBuyer buyer, SerializableDictionary<Product, int> ol)
    {
        Buyer = buyer;
        orderingList = ol;
    }

    public bool Equals(Message other)
    {
        return other.id == id;
    }

    public override int GetHashCode()
    {
        return this.id;
    }

    public void RemoveOrder(Product product)
    {
        orderingList.Remove(product);
    }
}
