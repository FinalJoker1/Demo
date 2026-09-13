using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyer 
{
    Action Unpassed { get; }
    bool Check(IProduct product);

    ICurrency Pay(ICurrency require);
}
