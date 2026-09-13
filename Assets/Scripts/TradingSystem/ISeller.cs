using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeller
{
    IProduct GetProduct();

    ICurrency Need();

    bool Check(ICurrency currency);

    Action Unpassed { get; }
}
