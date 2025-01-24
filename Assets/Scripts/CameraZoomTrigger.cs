using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作者：龚科翰
//创建时间: 2025年1月22日

/// <summary>
/// 相机动画触发器
/// 调节相机的大小和动画时间
/// </summary>
public class CameraZoomTrigger : MonoBehaviour
{
    public CameraZoom cameraZoomController; 
    /// <summary>
    /// 引用 CameraZoomController 脚本
    /// </summary>
    public float forwardZoom = 1f; 
    /// <summary>
    /// 区域内的目标缩放值
    /// </summary>
    public float transitionTime = 0.5f; 
    /// <summary>
    /// 动画平滑时间
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            /// <summary>
            /// 进入区域时调用缩放
            /// </summary>
            cameraZoomController.AdjustZoom(forwardZoom, transitionTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            /// <summary>
            /// 离开区域时重置为默认值
            /// </summary>
            cameraZoomController.ResetToDefault(transitionTime);
        }
    }
}
