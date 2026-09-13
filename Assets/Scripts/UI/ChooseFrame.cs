using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFrame : MonoBehaviour
{
    public Image SelfImage { get; private set; }
    public void Awake()
    {
        SelfImage = GetComponent<Image>();

    }

    public void Start()
    {
        SelfImage.sprite = AssetsLoader.products?.DefaultValue?.icon;
    }
}
