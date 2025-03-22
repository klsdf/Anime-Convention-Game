using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///作者：龚科翰
///日期：2025-03-03
///功能：作物数据   


/// <summary>
/// 作物数据 
/// </summary>
[CreateAssetMenu(fileName = "ItemDate/Seed")]
public class CropsData : ItemData
{
    public float[] timePerStage; // 每个阶段的生长时间
    public int harvestReward;   // 收割奖励（例如金币）
    public float witherTime = 10f; // 成熟后枯萎时间（可选）
}

