using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarming : MonoBehaviour
{
    /// 作者：龚科翰    
    /// 日期：2025-03-02

    /// <summary>
    /// 玩家种植
    /// </summary>
    // Start is called before the first frame update
    public Camera playerCamera;
    public LayerMask farmlandLayer;
    private bool isOnFarmland = false;
    private List<Collider> currentFarmlands = new List<Collider>();

    void Update()
    {
        Debug.Log("isOnFarmland:"+isOnFarmland);

        if (isOnFarmland && Input.touchCount > 0)
        {
            Debug.Log("isOnFarmland:"+isOnFarmland);
             Touch touch = Input.GetTouch(0);
            
            // 仅在触碰开始时检测
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = playerCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit, 100f, farmlandLayer))
                {
                    FarmLand plot = hit.collider.GetComponent<FarmLand>();
                    // ...原有耕地/种植逻辑
                    plot.SelectLand(true);  
                    Debug.Log("触发方块");
                }
                else
                {
                    FarmLand plot = hit.collider.GetComponent<FarmLand>();
                    plot.SelectLand(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (farmlandLayer == (farmlandLayer | (1 << other.gameObject.layer)))
        {
            currentFarmlands.Add(other);
            isOnFarmland = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (farmlandLayer == (farmlandLayer | (1 << other.gameObject.layer)))
        {
            currentFarmlands.Remove(other);
            if (currentFarmlands.Count == 0)
            {
                isOnFarmland = false;
            }
        }
    }
    /*
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // 仅在触碰开始时检测
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = playerCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit, 100f, farmlandLayer))
                {
                    FarmLand plot = hit.collider.GetComponent<FarmLand>();
                    // ...原有耕地/种植逻辑
                    plot.SelectLand(true);  
                    Debug.Log("触发方块");
                }
                else
                {
                    FarmLand plot = hit.collider.GetComponent<FarmLand>();
                    plot.SelectLand(false);
                }
            }
        }
    }
    */
}
