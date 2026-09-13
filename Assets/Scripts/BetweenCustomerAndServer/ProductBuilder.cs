using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBuilder : OrderlyBuilder<int,Product,ProductBuilder>
{
    public ProductBuilder(string path) : base(path) 
    {
        
    }

}
