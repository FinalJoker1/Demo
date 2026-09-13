using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingBuilder
{

    private Ordering newOrdering;

    public OrderingBuilder()
    {
        newOrdering = new Ordering();
    }
    public void AddFood(Product product)
    {
        if (newOrdering.orderingDict.ContainsKey(product))
            newOrdering.orderingDict[product]++;
        else
            newOrdering.orderingDict.Add(product, 1);

        newOrdering.cost += product.price; ;
    }

    public void RemoveFood(Product product)
    {
        if (newOrdering.orderingDict.ContainsKey(product) == false)
            return;

        newOrdering.orderingDict[product]--;
        newOrdering.cost -= product.price;

        if (newOrdering.orderingDict[product] <= 0)
            newOrdering.orderingDict.Remove(product);
    }

    public Ordering Build()
    {
        var tmp = newOrdering;
        Clear();
        return tmp;
    }

    public void Clear() => newOrdering = new Ordering();
}
