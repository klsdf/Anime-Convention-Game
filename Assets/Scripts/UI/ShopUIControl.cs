using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


///作者：龚科翰
///时间：2025年1月25日
///功能：商店UI控制
public class ShopUIControl : MonoBehaviour
{
   public GameObject shopCanvas;
/// <summary>
/// 商店 Canvas
/// </summary>
public GameObject gameUICanvas; 
/// <summary>
/// 游戏主界面 Canvas（可选）
/// </summary>

public Button openShopButton;
/// <summary>
/// 打开商店的按钮
/// </summary>
public Button closeShopButton; 
/// 关闭商店的按钮

private bool isShopOpen = false;

public Collider2D shopSpriteCollider; // Reference to the sprite's collider

private Camera mainCamera;

void Start()
{
    /// 给按钮添加监听事件
    if (openShopButton != null)
    {
        openShopButton.onClick.AddListener(OpenShop); /// 点击按钮时打开商店
       
    }
    if (closeShopButton != null)
    {
        closeShopButton.onClick.AddListener(CloseShop); ///点击按钮时关闭商店
    }

    mainCamera = Camera.main;

    /// 确保在开始时商店关闭
    if (shopCanvas != null)
    {
        shopCanvas.SetActive(false);
    }
}
 void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (shopSpriteCollider != null && shopSpriteCollider.OverlapPoint(mousePosition))
            {
                OpenShop();
            }
        }
    }

public void OpenShop()
{
    if (shopCanvas != null)
    {
        shopCanvas.SetActive(true); // 打开商店
        Debug.Log("The button trigger success");
    }

    if (gameUICanvas != null)
    {
        gameUICanvas.SetActive(false); // 隐藏主界面（可选）
    }

    Time.timeScale = 0; // 暂停游戏（可选）
    isShopOpen = true;
    Debug.Log("Shop opened!");
}

public void CloseShop()
{
    if (shopCanvas != null)
    {
        shopCanvas.SetActive(false); // 关闭商店
    }

    if (gameUICanvas != null)
    {
        gameUICanvas.SetActive(true); // 显示主界面（可选）
    }

    Time.timeScale = 1; // 恢复游戏逻辑（可选）
    isShopOpen = false;
    Debug.Log("Shop closed!");
}
}
