using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFrame : MonoBehaviour
{
    private Product product;
    private Image icon;
    private TMPro.TextMeshProUGUI textMeshPro;
    private Button button;

    public Product GetProduct() => this.product;
    public void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetProduct(Product product)
    {
        this.product = product;
        icon = GetComponentInChildren<Image>();
        textMeshPro = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        icon.sprite = product.icon;
        textMeshPro.text = product.description;
    }
    
}
