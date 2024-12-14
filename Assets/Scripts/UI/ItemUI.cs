//作者：闫辰祥
//创建时间：2024年12月14日

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 物品UI组件，处理物品的显示和交互效果
/// </summary>
public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("高亮设置")]
    [SerializeField] private float highlightIntensity = 0.2f; // 高亮增加的亮度
    [SerializeField] private Color highlightColor = new Color(1f, 0.84f, 0f, 1f); // 金色高亮
    [SerializeField] private float highlightTransitionSpeed = 5f; // 高亮过渡速度
    [SerializeField] [Range(0f, 1f)] private float colorBlendStrength = 0.3f; // 金色混合强度

    private Image[] allImages;
    private Color[] originalColors;
    private Color[] targetColors;
    private bool isHighlighted = false;

    private void Awake()
    {
        allImages = GetComponentsInChildren<Image>(true);
        originalColors = new Color[allImages.Length];
        targetColors = new Color[allImages.Length];

        for (int i = 0; i < allImages.Length; i++)
        {
            originalColors[i] = allImages[i].color;
            targetColors[i] = originalColors[i];
        }
    }

    private void Update()
    {
        for (int i = 0; i < allImages.Length; i++)
        {
            allImages[i].color = Color.Lerp(allImages[i].color, targetColors[i], Time.deltaTime * highlightTransitionSpeed);
        }
    }

    /// <summary>
    /// 当鼠标进入UI时调用
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true;
        for (int i = 0; i < allImages.Length; i++)
        {
            // 增加亮度并混合金色
            Color baseColor = originalColors[i] * (1 + highlightIntensity);
            Color blendedColor = Color.Lerp(baseColor, highlightColor, colorBlendStrength);
            // 保持原始透明度
            blendedColor.a = originalColors[i].a;
            targetColors[i] = blendedColor;
        }
    }

    /// <summary>
    /// 当鼠标离开UI时调用
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;
        for (int i = 0; i < allImages.Length; i++)
        {
            targetColors[i] = originalColors[i];
        }
    }

    /// <summary>
    /// 手动设置高亮状态
    /// </summary>
    public void SetHighlight(bool highlight)
    {
        if (highlight != isHighlighted)
        {
            if (highlight)
            {
                OnPointerEnter(null);
            }
            else
            {
                OnPointerExit(null);
            }
        }
    }
}
