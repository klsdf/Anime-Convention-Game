using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    // 作物状态枚举
    public enum CropState
    {
        Planted,  // 播种
        Growing,  // 生长
        Mature,   // 成熟（可收割）
        Regrowing, // 再生中
        Withered // 枯萎
    }

    public CropsData cropData;
    public GameObject[] growthStages;

    private CropState currentState;
    private int currentStage;
    private float growthProgress;
    private float stateTimer; // 通用状态计时器
    private int remainingHarvests;
    private FarmLand parentFarmLand;

    void Start()
    {
        InitializeCrop();
    }

    private void InitializeCrop()
    {
        parentFarmLand = GetComponentInParent<FarmLand>();
        if (!ValidateComponents()) return;

        currentState = CropState.Planted;
        currentStage = 0;
        growthProgress = 0f;
        remainingHarvests = cropData.regrowable ? cropData.regrowCount + 1 : 1;
        UpdateStageModel();
    }

    private bool ValidateComponents()
    {
        if (cropData == null || growthStages == null || growthStages.Length == 0)
        {
            Debug.LogError("作物数据或阶段模型未设置！");
            return false;
        }
        if (parentFarmLand == null)
        {
            Debug.LogError("父对象 FarmLand 未设置！");
            return false;
        }
        return true;
    }

    void Update()
    {
        switch (currentState)
        {
            case CropState.Planted:
                TransitionToState(CropState.Growing);
                break;

            case CropState.Growing:
                UpdateGrowth();
                break;

            case CropState.Mature:
                UpdateMatureState();
                break;

            case CropState.Regrowing:
                UpdateRegrowth();
                break;

            case CropState.Withered:
                HandleWitheredState();
                break;
        }
    }

    private void UpdateGrowth()
    {
        growthProgress += Time.deltaTime;

        if (growthProgress >= cropData.timePerStage[currentStage])
        {
            growthProgress = 0f;
            currentStage++;

            if (currentStage >= growthStages.Length - 1)
            {
                TransitionToState(CropState.Mature);
            }
            else
            {
                UpdateStageModel();
            }
        }
    }

    private void UpdateMatureState()
    {
        stateTimer += Time.deltaTime;
        if (stateTimer > cropData.witherTime)
        {
            TransitionToState(CropState.Withered);
        }
    }

    private void UpdateRegrowth()
    {
        stateTimer += Time.deltaTime;
        if (stateTimer >= cropData.regrowDelay)
        {
            TransitionToState(CropState.Mature);
        }
    }

    private void HandleWitheredState()
    {
        Debug.Log("作物已枯萎，不可收割！");
        DestroyCrop();
    }

    public void Harvest()
    {
        if (!IsReadyToHarvest()) return;

        Debug.Log($"作物已收割！剩余收割次数: {remainingHarvests - 1}");
        // PlayerInventory.AddResource(cropData.harvestReward);

        remainingHarvests--;

        if (remainingHarvests > 0 && cropData.regrowable)
        {
            TransitionToState(CropState.Regrowing);
        }
        else
        {
            DestroyCrop();
        }
    }

    private void TransitionToState(CropState newState)
    {
        currentState = newState;
        stateTimer = 0f;

        switch (newState)
        {
            case CropState.Mature:
                currentStage = growthStages.Length - 1; // 强制显示成熟阶段
                Debug.Log($"作物成熟，剩余收割次数: {remainingHarvests}");
                break;

            case CropState.Regrowing:
                Debug.Log($"开始再生，等待时间: {cropData.regrowDelay}秒");
                break;
        }

        UpdateStageModel();
    }

    private void UpdateStageModel()
    {
        for (int i = 0; i < growthStages.Length; i++)
        {
            bool shouldShow = (i == currentStage) || 
                            (currentState == CropState.Mature && i == growthStages.Length - 1);
            
            if (growthStages[i] != null)
            {
                growthStages[i].SetActive(shouldShow);
            }
        }
    }

    private void DestroyCrop()
    {
        ResetLand();
        Destroy(gameObject);
    }

    private void ResetLand()
    {
        parentFarmLand.SwitchLandStatus(FarmLand.LandState.Soil);
        parentFarmLand.isPlanted = false;
    }

    public bool IsReadyToHarvest(){
        return currentState == CropState.Mature;
    }
}


