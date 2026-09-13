using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    public Image currencyImage;
    public TMPro.TextMeshProUGUI valueTXT;
    public float intervelTime = 0.02f;

    public void Start()
    {
        StartCoroutine(UIUpdate());
    }

    private IEnumerator UIUpdate()
    {
        float flag = Time.time;

        while (true)
        {
            valueTXT.text = (AssetsLoader.player.Property.Currency as int?).ToString();
            flag = Time.time + intervelTime;
            while(Time.time < flag)
                yield return null;
        }
    }
}
