using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 作者：龚科翰
/// 日期：2025-03-02

/// <summary>
/// 农场土地
/// </summary>
public class FarmLand : MonoBehaviour
{
    public enum LandState
    {
        Soil,
        Farmland,
        Water
    }
    // Start is called before the first frame update
    public LandState landState;

    /// <summary>
    /// 土地状态
    /// </summary>
    public Material soilMat, farmlandMat, WaterMat;

    /// <summary>
    /// 渲染器
    /// </summary>
    new Renderer renderer;

    /// <summary>
    /// 检测显示选中地块
    /// </summary>
    public GameObject select;

    /// <summary>
    /// 作物预制体
    /// </summary>
    public CropsData cropData;
    
    /// <summary>
    /// 是否种植作物在这个土地上
    /// </summary>
    private bool isPlanted = false;

    /// <summary>
    /// 种植的作物
    /// </summary>
    private CropBehavior currentCrop;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandState.Soil);
    }

    /// <summary>
    /// 切换土地状态
    /// </summary>
    public void SwitchLandStatus(LandState statusToSwitch)
    {
        landState = statusToSwitch;

        Material materialToSwitch = soilMat;
        switch(statusToSwitch)
        {
            case LandState.Soil:
                materialToSwitch = soilMat;
                break;
            case LandState.Farmland:
                materialToSwitch = farmlandMat;
                break;
            case LandState.Water:
                materialToSwitch= WaterMat;
                break;
        }
        //获取材质并切换
        renderer.material = materialToSwitch;
    }

    public void SelectLand(bool toggle)
    {
        select.SetActive(toggle);
       
    }

    public void PlantCrop(CropsData data)
    {
        // 如果土地状态不是Soil，则可以种植
        if(landState != LandState.Soil && !isPlanted)
        {
            cropData = data;
            isPlanted = true;
            // 创建作物预制体
            GameObject crop = Instantiate(cropData.prefab, transform.position, Quaternion.identity);
            // 设置作物预制体为土地的子物体
            crop.transform.SetParent(transform);
            // 添加作物行为组件
            currentCrop = crop.GetComponent<CropBehavior>();
            // 设置作物数据
            currentCrop.cropData = cropData;
        }
        else
        {
            Debug.Log("土地状态不能种植");
        }

        
    }
}
