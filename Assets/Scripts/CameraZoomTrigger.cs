using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraZoomTrigger : MonoBehaviour
{
    public CameraZoom cameraZoomController; // 引用 CameraZoomController 脚本
    public float forwardZoom = 1f; // 区域内的目标缩放值
    public float transitionTime = 0.5f; // 动画平滑时间

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 进入区域时调用缩放
            cameraZoomController.AdjustZoom(forwardZoom, transitionTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 离开区域时重置为默认值
            cameraZoomController.ResetToDefault(transitionTime);
        }
    }
}
