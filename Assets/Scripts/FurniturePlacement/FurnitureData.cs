using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作者：龚科翰
/// 日期：2025-03-31
/// 功能：家具数据
/// </summary>
public enum FurnitureType
{
    Floor,
    Wall,
    Ceiling
}

[CreateAssetMenu(fileName = "FurnitureData", menuName = "Furniture/Data")]
public class FurnitureData : ScriptableObject
{
   [Header("基础属性")]
    public string id;                // 唯一标识符
    public string displayName;       // 显示名称
    public GameObject prefab;       // 关联的预制体
    public Vector2Int gridSize;     // 占用网格单位（如2x3）

    [Header("放置设置")]
    public LayerMask placementLayer; // 允许放置的表面层级
    public FurnitureType type;       // 家具类型
    public bool allowRotation;       // 是否允许旋转
}