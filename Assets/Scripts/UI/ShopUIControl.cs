using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShopUIControl : MonoBehaviour
{
    public GameObject shopCanvas; // 商店 Canvas
    public GameObject gameUICanvas; // 游戏主界面 Canvas（可选）

    public Button openShopButton; // 打开商店的按钮
    public Button closeShopButton; // 关闭商店的按钮

    private bool isShopOpen = false;

    void Start()
    {
        // 给按钮添加监听事件
        if (openShopButton != null)
        {
            openShopButton.onClick.AddListener(OpenShop); // 点击按钮时打开商店
           
        }
        if (closeShopButton != null)
        {
            closeShopButton.onClick.AddListener(CloseShop); // 点击按钮时关闭商店
        }

        // 确保在开始时商店关闭
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(false);
        }
    }
    void Update()
    {
        // 检测按下 'E' 键时打开商店
        if (Input.GetKeyDown(KeyCode.E) && !isShopOpen)
        {
            OpenShop();
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
