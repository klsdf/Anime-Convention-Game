using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShopUIControl : MonoBehaviour
{
    public GameObject shopCanvas; // �̵� Canvas
    public GameObject gameUICanvas; // ��Ϸ������ Canvas����ѡ��

    public Button openShopButton; // ���̵�İ�ť
    public Button closeShopButton; // �ر��̵�İ�ť

    private bool isShopOpen = false;

    void Start()
    {
        // ����ť��Ӽ����¼�
        if (openShopButton != null)
        {
            openShopButton.onClick.AddListener(OpenShop); // �����ťʱ���̵�
           
        }
        if (closeShopButton != null)
        {
            closeShopButton.onClick.AddListener(CloseShop); // �����ťʱ�ر��̵�
        }

        // ȷ���ڿ�ʼʱ�̵�ر�
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(false);
        }
    }
    void Update()
    {
        // ��ⰴ�� 'E' ��ʱ���̵�
        if (Input.GetKeyDown(KeyCode.E) && !isShopOpen)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(true); // ���̵�
            Debug.Log("The button trigger success");
        }

        if (gameUICanvas != null)
        {
            gameUICanvas.SetActive(false); // ���������棨��ѡ��
        }

        Time.timeScale = 0; // ��ͣ��Ϸ����ѡ��
        isShopOpen = true;
        Debug.Log("Shop opened!");
    }

    public void CloseShop()
    {
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(false); // �ر��̵�
        }

        if (gameUICanvas != null)
        {
            gameUICanvas.SetActive(true); // ��ʾ�����棨��ѡ��
        }

        Time.timeScale = 1; // �ָ���Ϸ�߼�����ѡ��
        isShopOpen = false;
        Debug.Log("Shop closed!");
    }
}
