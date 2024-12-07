using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text buttonText; // 按钮上的文本组件

    private void Awake() {
        if (buttonText == null) {
            buttonText = GetComponentInChildren<TMP_Text>();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = new Color32(150, 150, 255, 255);
            buttonText.fontStyle = FontStyles.Underline;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = new Color32(255, 255, 255, 255); // 恢复原始颜色
            buttonText.fontStyle = FontStyles.Normal;
        }
    }
}