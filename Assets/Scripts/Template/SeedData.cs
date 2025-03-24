using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///作者：龚科翰
///日期：2025-03-03
///功能：作物数据   


/// <summary>
/// 作物数据 
/// </summary>
[CreateAssetMenu(fileName = "ItemData/Seed", menuName = "Farming/CropsData")]
public class CropsData : ScriptableObject
{
    public SaveableItemData saveableItemData;
    public GameObject prefab;
    public float[] timePerStage; // 每个阶段的生长时间
    public int harvestReward;   // 收割奖励（例如金币）
    public float witherTime = 10f; // 成熟后枯萎时间（可选）
     
    [Header("再生")]
    public bool regrowable = false; // 是否可再生
    public int regrowCount = 1; // 再生次数
    public float regrowDelay = 10f; // 再生延迟时间
}

