using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera; // 引用 Cinemachine 虚拟相机
    private float defaultZoom; // 默认初始缩放值
    private float currentZoom; // 当前相机的缩放值
    private float forwardZoom; // 下一次缩放目标值
    private int triggerCount = 0; // 用来跟踪玩家进入的触发器数量

    private Tween zoomTween; // 控制缩放动画

    private void Start()
    {
        // 初始化默认值为当前相机的正交大小
        defaultZoom = cinemachineCamera.m_Lens.OrthographicSize;
        currentZoom = defaultZoom; // 初始时当前缩放值等于默认值
    }

    /// <summary>
    /// 调整相机缩放至指定目标值
    /// </summary>
    /// <param name="forwardSize">目标缩放值（最终值）</param>
    /// <param name="transitionTime">平滑过渡时间</param>
    public void AdjustZoom(float forwardSize, float transitionTime)
    {
        // 增加触发器计数
        triggerCount++;

        // 设置目标缩放值
        forwardZoom = forwardSize;

        // 停止之前的缩放动画（如果有）
        zoomTween?.Kill();

        // 启动新的缩放动画
        zoomTween = DOTween.To(
            () => cinemachineCamera.m_Lens.OrthographicSize,
            value => cinemachineCamera.m_Lens.OrthographicSize = value,
            forwardZoom,
            transitionTime
        ).OnComplete(() =>
        {
            // 动画完成后，将当前缩放值更新为目标值
            currentZoom = forwardZoom;
        });
    }

    /// <summary>
    /// 重置相机缩放到默认值
    /// </summary>
    /// <param name="transitionTime">平滑过渡时间</param>
    public void ResetToDefault(float transitionTime)
    {
        // 减少触发器计数
        triggerCount--;

        // 只有当计数为 0 时，才执行重置
        if (triggerCount <= 0)
        {
            triggerCount = 0; // 确保计数不为负
            zoomTween?.Kill();
            zoomTween = DOTween.To(
                () => cinemachineCamera.m_Lens.OrthographicSize,
                value => cinemachineCamera.m_Lens.OrthographicSize = value,
                defaultZoom,
                transitionTime
            ).OnComplete(() =>
            {
                currentZoom = defaultZoom;
            });
        }
    }
}
