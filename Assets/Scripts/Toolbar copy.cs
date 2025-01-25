using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Toolbar : SlotUI
{
    public Sprite SLotDark;
    public Sprite SlotLight;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Highlight()
    {
        image.sprite = SLotDark;

    }

    public void Unhighlight()
    {
        image.sprite = SlotLight;
    }
}
