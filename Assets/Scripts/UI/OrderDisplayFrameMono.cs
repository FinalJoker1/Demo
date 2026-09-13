using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplayFrameMono : MonoBehaviour
{
    public Image Icon { get; private set; }
    public TMPro.TextMeshProUGUI TXT { get; private set; }
    public string spriteGOName = "Sprite";
    public string txtGOName = "Count";
    public OrderDisplayFrame selfFrame;
    public void Awake()
    {
        Icon = transform.Find(spriteGOName)?.GetComponent<Image>();
        TXT = Icon.transform.Find(txtGOName)?.GetComponent<TMPro.TextMeshProUGUI>();
    }
}
