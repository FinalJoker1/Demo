using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 交易系统的货币实现类
/// </summary>
public class Money : ICurrency
{
    [JsonProperty] private int? value;

    public Money()
    {

    }
    public Money(int? value):this()
    {
        this.value = value;
    }
    [JsonIgnore] public object Currency => this.value;

    public override bool Equals(object obj)
    {
        if (obj is Money == false)
            return false;

        return value.Equals((obj as Money)?.value);
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public void PlusCurrency(ICurrency currency)
    {
        var addValue = currency.Currency as int?;
        if (addValue != null)
        {
            value += addValue;
        }
    }
}
