using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SlotUI : MonoBehaviour
{ 
    private SlotData data;
    public int index = 0;

    public Image Iconimage;
    public TextMeshProUGUI countText;

    public void SetData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDataChange);

        updateUI();
        
    }

    private void OnDataChange()
    {
        updateUI();
    }

    private void updateUI()
    {
        

        if (data.item == null)
        {
            Iconimage.enabled = false;
            countText.enabled = false;
        }

        else
        {
            Iconimage.enabled = true;
            countText.enabled = true;
            Iconimage.sprite = data.item.sprite;
            countText.text = data.count.ToString();
        }
    }
}
