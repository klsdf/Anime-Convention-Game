using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    private static ShopPanel instance;
    
    [SerializeField] 
    private Canvas shopCanvas;
    
    private void Awake()
    {
        instance = this;
        // 初始时隐藏商店界面
        shopCanvas.enabled = false;
    }
    
    public static void Show()
    {
        if (instance != null)
        {
            instance.shopCanvas.enabled = true;
        }
    }
    
    public void Hide()
    {
        shopCanvas.enabled = false;
    }
    
    // 关闭按钮调用此方法
    public void OnCloseButtonClick()
    {
        Hide();
    }
} 