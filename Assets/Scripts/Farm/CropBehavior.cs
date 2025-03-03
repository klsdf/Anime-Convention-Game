using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    public CropsData cropData; // 作物数据
    private int currentStage = 0; // 当前生长阶段
    private float growthProgress = 0f; // 生长进度

    void Start()
    {
        // 初始化作物，显示第一阶段模型
        UpdateStageModel();
    }

    void Update()
    {
        // 如果作物未成熟，则更新生长进度   
        if (currentStage < cropData.growthStages.Length - 1)
        {
            // 更新生长进度
            // 这个功能暂定为用游戏时间来更新生长进度
            // 之后应该会尝试去获取现实时间来更新生长进度
            growthProgress += Time.deltaTime;

            // 检查是否需要切换到下一个阶段
            if (growthProgress >= cropData.growthTime / cropData.growthStages.Length)
            {
                currentStage++;
                UpdateStageModel();
                growthProgress = 0f; // 重置生长进度
            }
        }
    }

    // 更新当前阶段的模型
    void UpdateStageModel()
    {
        // 隐藏所有阶段的模型
        foreach (var stage in cropData.growthStages)
        {
            stage.SetActive(false);
        }

        // 显示当前阶段的模型
        cropData.growthStages[currentStage].SetActive(true);
    }

    // 检查作物是否成熟
    public bool IsReadyToHarvest()
    {
        return currentStage == cropData.growthStages.Length - 1;
    }
}
