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
        if(landState == LandState.Water && !isPlanted)
        {
            if (data.prefab == null)
            {
                Debug.LogError("错误：作物预制体未配置！");
                return;
            }
           
        // 生成作物（调整Y轴高度）
        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        GameObject crop = Instantiate(data.prefab, spawnPosition, Quaternion.identity);
        
        // 可选：设为土地的子物体（确保土地缩放为1）
        crop.transform.SetParent(transform);

        currentCrop = crop.GetComponent<CropBehavior>();
        if (currentCrop == null)
        {
            Debug.LogError("作物预制体缺少 CropBehavior 组件！");
            Destroy(crop);
            isPlanted = false;
            landState = LandState.Water; // 回滚状态
            return;
        }

        // 更新土地状态和种植标记
        isPlanted = true;
        currentCrop.cropData = data;
        Debug.Log("成功种植: " + cropData.name);
    }
    else
    {
        Debug.Log("土地状态不能种植");
    }
    }
}
