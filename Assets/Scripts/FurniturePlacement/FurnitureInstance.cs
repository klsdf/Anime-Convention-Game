using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作者：龚科翰
/// 时间：2025-04-04
/// 家具实例
/// </summary>  

public class FurnitureInstance : MonoBehaviour
{ 
   public FurnitureData data;
   public Collider mainCollider;
    
    public void Initialize(FurnitureData data)
    {
        this.data = data;
        name = data.displayName;
    }

}
