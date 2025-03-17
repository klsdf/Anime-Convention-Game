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
    public CropsData selectedCropData;   // 玩家选择的作物数据（可从UI或背包系统获取）   

    void Update()
    {
        /// 检测玩家是否在耕地范围内    
        if (isOnFarmland && Input.touchCount > 0)
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
                    if (plot != null)
                    {
                        ///优化：可以加入检查背包工具作为条件判断是耕种还是浇水
                        if(plot.landState == FarmLand.LandState.Soil)
                        {
                            ///耕地
                            plot.SwitchLandStatus(FarmLand.LandState.Farmland);
                            ///这里可以加入一个耕种动画
                        }
                        else if(plot.landState == FarmLand.LandState.Farmland)
                        {
                            ///种植
                            plot.SwitchLandStatus(FarmLand.LandState.Water);
                            ///这里可以加入一个浇水动画
                        }
                     
                        else if(plot.landState == FarmLand.LandState.Water)
                        {
                            ///种植
                            ///初始的种植之后会调用数据库中的种子
                            TryPlantCrop(plot);
                            Debug.Log("种植");
                            ///这里可以加入一个种植动画
                            ///这里可以加入一个种植音效 
                            ///这里可以加入一个种植特效
                        }
                    }
                }
            }
        }
      

        // 处理currentFarmlands列表中的对象
        foreach (Collider collider in currentFarmlands)
        {
            FarmLand farmLand = collider.GetComponent<FarmLand>();
            if (farmLand != null)
            {
                farmLand.SelectLand(true); // 调用SelectLand方法
               
            }
        }
    }
     
    /// <summary>
    /// 检测玩家是否进入耕地范围
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (farmlandLayer == (farmlandLayer | (1 << other.gameObject.layer)))
        {
            currentFarmlands.Add(other);
            isOnFarmland = true;
        }
    }
    /// <summary>
    /// 检测玩家是否离开耕地范围
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
         if (farmlandLayer == (farmlandLayer | (1 << other.gameObject.layer)))
        {
            // 调用SelectLand(false)
            FarmLand farmLand = other.GetComponent<FarmLand>();
            if (farmLand != null)
            {
                farmLand.SelectLand(false);
                Debug.Log("离开FarmLand: " + other.name);
            }

            // 从列表中移除
            currentFarmlands.Remove(other);

            // 如果列表为空，设置isOnFarmland为false
            if (currentFarmlands.Count == 0)
            {
                isOnFarmland = false;
            }
        }
    }
    
     private void TryPlantCrop(FarmLand farmLand)
    {

                if (farmLand != null && selectedCropData != null)
                {
                    // 调用FarmLand的PlantCrop方法
                    farmLand.PlantCrop(selectedCropData);
                }
                else
                {
                    Debug.Log("未选择作物或目标不是耕地！");
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
