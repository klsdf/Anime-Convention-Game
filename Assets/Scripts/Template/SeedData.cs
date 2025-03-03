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
    public float growthTime; // 总生长时间（秒）
    public GameObject[] growthStages; // 各阶段模型 
    public int harvestCount; // 收获数量
    public int seedPrice; // 种子价格
}

