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


    private CropsData selectedCropData;   // 玩家选择的作物数据（可从UI或背包系统获取）应该调用背包系统里的种子数据
    public CropsData SelectedCrop => selectedCropData; // 只读属性
    private CropBehavior currentCropSelected;

    /// <summary>
    /// 更新 
    /// </summary>
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
                        else if(plot.landState == FarmLand.LandState.Occupied)
                        {
                            ///获取作物
                            currentCropSelected = plot.transform.GetChild(1).GetComponent<CropBehavior>();
                            if (currentCropSelected != null)
                            {
                                currentCropSelected.Harvest();                                                       
                                Debug.Log("收割");
                                // 这里可以加入一个收割动画
                                // 这里可以加入一个收割音效
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
    
    /// <summary>
    /// 直接从SaveItemUI调用的选择方法（已跳过前置检查）
    /// </summary>
    public void SelectSeedFromInventory(SaveableItemData seedItem)
    {
        // 直接信任传入的数据（因为SaveItemUI已验证）
        selectedCropData = seedItem._cropData;
        Debug.Log($"种子选中: {selectedCropData.saveableItemData.itemName}");
        
        // 可选：触发选中事件
        // OnSeedSelected?.Invoke(_selectedCropData);
    }
    /// <summary>
    /// 清除当前选择
    /// </summary>
    public void ClearSelection()
    {
        selectedCropData = null;
    }

    /// <summary>
    /// 尝试种植当前选中的种子
    /// </summary>
    public bool TryPlantSelectedSeed(FarmLand targetLand)
    {
        if (selectedCropData == null || targetLand == null)
            return false;

        targetLand.PlantCrop(selectedCropData);
        ClearSelection();
        return true;
    }
}
