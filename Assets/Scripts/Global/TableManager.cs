using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager类一般放在一个对全体实例公开的地方
/// </summary>
public class TableManager : MonoBehaviour
{
    public SerializableHashSet<Table> usableTables = new SerializableHashSet<Table>();

    public bool IsTableEnough() => usableTables.Count > 0;

    public bool SelectTable(Customer customer)
    {
        if (IsTableEnough() == false)
            return false;

        var iter = usableTables.GetEnumerator();
        iter.MoveNext();
        customer.table = iter.Current;
        if((--iter.Current.remainingCapacity) <= 0)
            usableTables.Remove(iter.Current);

        return true;

    }

    public void ReturnTable(Customer customer)
    {
        if (customer.table == null)
            return;

        var returnTable = customer.table;
        returnTable.remainingCapacity = Mathf.Max(returnTable.maxCapacity, returnTable.remainingCapacity + 1);
        
        if(usableTables.Contains(returnTable) == false)
            usableTables.Add(returnTable);
    }
}
