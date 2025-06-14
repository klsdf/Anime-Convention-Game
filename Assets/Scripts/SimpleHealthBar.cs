using UnityEngine;

public class SimpleHealthBar : MonoBehaviour
{
    public Transform fill;  // 血条填充部分
    public Color fullColor = Color.green;
    public Color lowColor = Color.red;
    public float lowHealthThreshold = 0.3f; // 低于30%血量时变红
    
    private SpriteRenderer fillRenderer;
    
    void Start()
    {
        // 如果没有指定fill，尝试找到一个名为"Fill"的子对象
        if (fill == null)
        {
            Transform fillTransform = transform.Find("Fill");
            if (fillTransform != null)
            {
                fill = fillTransform;
            }
        }
        
        // 获取填充部分的SpriteRenderer
        if (fill != null)
        {
            fillRenderer = fill.GetComponent<SpriteRenderer>();
        }
    }
    
    // 更新血条显示
    public void UpdateHealthBar(float healthPercent)
    {
        if (fill != null)
        {
            // 更新填充宽度
            Vector3 scale = fill.localScale;
            scale.x = Mathf.Clamp01(healthPercent);
            fill.localScale = scale;
            
            // 更新颜色
            if (fillRenderer != null)
            {
                fillRenderer.color = healthPercent <= lowHealthThreshold ? lowColor : fullColor;
            }
        }
    }
}