using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    private Image backgroundImage;
    private Text valueText;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        valueText = GetComponentInChildren<Text>();
    }

    public void SetValue(float value)
    {
        if (valueText != null)
        {
            valueText.text = value.ToString("F1");
        }
    }

    public void SetColor(Color color)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = color;
        }
    }
} 