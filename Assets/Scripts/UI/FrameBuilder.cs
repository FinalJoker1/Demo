using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameBuilder
{
    public IImage Begain(GameObject prefab) => new Constructor(prefab);
    private class Constructor : IImage, IText, IProductInfo, IFrameBuild,IBuyerInfo
    {
        public Constructor(GameObject prefab)
        {
            displayFrame.frame = prefab;
        }
        private OrderDisplayFrame displayFrame = new OrderDisplayFrame();
        public OrderDisplayFrame Build()
        {
            return displayFrame;
        }

        public IText GetImage(Image image)
        {
            displayFrame.iconShow = image;
            return this;
        }

        public IBuyerInfo GetProduct(Product product, int count)
        {
            displayFrame.product = product;
            displayFrame.count = count;
            return this;
        }

        public IProductInfo GetTextMeshProUGUI(TextMeshProUGUI tmp)
        {
            displayFrame.txtShow = tmp;
            return this;
        }

        public IFrameBuild GetBuyer(IBuyer buyer)
        {
            displayFrame.Buyer = buyer;
            return this;
        }
    }
}

public interface IImage
{
    IText GetImage(Image image);
}

public interface IText
{
    IProductInfo GetTextMeshProUGUI(TMPro.TextMeshProUGUI tmp);
}

public interface IProductInfo
{
    IBuyerInfo GetProduct(Product product, int count);
}

public interface IBuyerInfo
{
    IFrameBuild GetBuyer(IBuyer buyer);
}
public interface IFrameBuild
{
    OrderDisplayFrame Build();
}



