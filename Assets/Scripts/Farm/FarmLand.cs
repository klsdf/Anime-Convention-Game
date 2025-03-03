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

    public Material soilMat, farmlandMat, WaterMat;

    new Renderer renderer;

    /// <summary>
    /// 检测显示选中地块
    /// </summary>
    public GameObject select;

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
}
