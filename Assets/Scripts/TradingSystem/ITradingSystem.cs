using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITradingSystem<S,B>
{
    S Seller { get;}
    B Buyer { get; }

    bool Trade(Action<object> action);
}
