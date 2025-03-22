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
        Mature,   // 成熟
        Withered // 枯萎（可选）
    }

    public CropsData cropData; // 作物数据
    public GameObject[] growthStages; // 每个阶段的模型

    private CropState currentState; // 当前状态
    private int currentStage;       // 当前生长阶段
    private float growthProgress;   // 生长进度
    private float matureTimer;      // 成熟后计时器（用于枯萎逻辑）

    void Start()
    {
        if (cropData == null || growthStages == null || growthStages.Length == 0)
        {
            Debug.LogError("作物数据或阶段模型未设置！");
            return;
        }

        // 初始状态：播种
        currentState = CropState.Planted;
        currentStage = 0;
        growthProgress = 0f;
        UpdateStageModel();
    }

    void Update()
    {
        // 根据当前状态执行逻辑
        switch (currentState)
        {
            case CropState.Planted:
                // 播种后进入生长状态
                currentState = CropState.Growing;
                break;

            case CropState.Growing:
                UpdateGrowth();
                break;

            case CropState.Mature:
                // 成熟状态，检查是否枯萎
                matureTimer += Time.deltaTime;
                if (matureTimer > cropData.witherTime)
                {
                    currentState = CropState.Withered;
                    UpdateStageModel();
                    Debug.Log("作物已枯萎！");
                }
                break;

            case CropState.Withered:
                 //枯萎状态，不可收割
                Debug.Log("作物已枯萎，不可收割！");
                Destroy(gameObject);
                break;
        }
    }

    // 更新生长逻辑
    void UpdateGrowth()
    {
        if (currentStage >= growthStages.Length - 1)
        {
            // 生长完成，进入成熟状态
            currentState = CropState.Mature;
            Debug.Log("作物已成熟！");
            return;
        }

        growthProgress += Time.deltaTime;
        Debug.Log($"当前生长进度: {growthProgress}");

        if (growthProgress >= cropData.timePerStage[currentStage])
        {
            currentStage++;
            UpdateStageModel();
            growthProgress = 0f;
            Debug.Log($"进入阶段 {currentStage}");
        }
    }

    // 更新当前阶段的模型
    void UpdateStageModel()
    {
        // 隐藏所有阶段的模型
        for (int i = 0; i < growthStages.Length; i++)
        {
            if (growthStages[i] != null)
            {
                growthStages[i].SetActive(false);
            }
        }

        // 显示当前阶段的模型
        if (growthStages[currentStage] != null)
        {
            growthStages[currentStage].SetActive(true);
            Debug.Log($"显示阶段模型: {growthStages[currentStage].name}");
        }
        else
        {
            Debug.LogError($"阶段 {currentStage} 的模型未配置！");
        }
    }

    // 检查作物是否成熟
    public bool IsReadyToHarvest()
    {
        return currentState == CropState.Mature;
    }

    // 收割作物
    public void Harvest()
    {
        if (IsReadyToHarvest())
        {
            Debug.Log("作物已收割！");
            // 增加玩家资源
            //PlayerInventory.AddResource(cropData.harvestReward);
            Destroy(gameObject); // 销毁作物
        }
        else
        {
            Debug.Log("作物尚未成熟，无法收割！");
        }
    }
}


