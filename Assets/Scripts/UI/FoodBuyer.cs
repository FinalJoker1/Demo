using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : IProduct
{
    private Product food;

    public Food(Product food)
    {
        this.food = food;
    }

    public object Product => food;
}

public class FoodBuyer : IBuyer
{
    public Action Unpassed => throw new NotImplementedException();

    public bool Check(IProduct product)
    {
        throw new NotImplementedException();
    }

    public ICurrency Pay(ICurrency require)
    {
        throw new NotImplementedException();
    }
}
