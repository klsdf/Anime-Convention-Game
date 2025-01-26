using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
//作者：龚科翰
//创建时间: 2025年1月22日

/// <summary>
/// 相机的动画控制器
/// 避免动画出现冲突
/// (相机的动画切换功能还需要修改)
/// </summary>
public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera; 
    /// <summary>
    /// 引用 Cinemachine 虚拟相机
    /// </summary>
    private float defaultZoom; 
    /// <summary>
    /// 默认初始缩放值
    /// </summary>
    private float currentZoom; 
    /// <summary>
    /// 当前相机的缩放值
    /// </summary>
    private float forwardZoom; 
    /// <summary>
    /// 下一次缩放目标值
    /// </summary>
    private int triggerCount = 0; 
    /// <summary>
    /// 用来跟踪玩家进入的触发器数量
    /// </summary>

    private Tween zoomTween; 
    /// <summary>
    /// 控制缩放动画
    /// </summary>

    private void Start()
    {
        
        /// <summary>
        /// 初始化默认值为当前相机的正交大小
        /// </summary>
        defaultZoom = cinemachineCamera.m_Lens.OrthographicSize;
        currentZoom = defaultZoom; 
        /// <summary>
        /// 初始时当前缩放值等于默认值
        /// </summary>
    }

    /// <summary>
    /// 调整相机缩放至指定目标值
    /// </summary>
    /// <param name="forwardSize">目标缩放值（最终值）</param>
    /// <param name="transitionTime">平滑过渡时间</param>
    public void AdjustZoom(float forwardSize, float transitionTime)
    {
        /// <summary>
        /// 增加触发器计数
        /// </summary>
        triggerCount++;
 
        /// <summary>
        /// 设置目标缩放值
        /// </summary>
        forwardZoom = forwardSize;

        /// <summary>
        /// 停止之前的缩放动画（如果有）
        /// </summary>
        zoomTween?.Kill();

        /// <summary>
        /// 启动新的缩放动画
        /// </summary>
        zoomTween = DOTween.To(
            () => cinemachineCamera.m_Lens.OrthographicSize,
            value => cinemachineCamera.m_Lens.OrthographicSize = value,
            forwardZoom,
            transitionTime
        ).OnComplete(() =>
        {
            /// <summary>
            /// 动画完成后，将当前缩放值更新为目标值
            /// </summary>
            currentZoom = forwardZoom;
        });
    }

    /// <summary>
    /// 重置相机缩放到默认值
    /// </summary>
    /// <param name="transitionTime">平滑过渡时间</param>
    public void ResetToDefault(float transitionTime)
    {
        /// <summary>
        /// 减少触发器计数
        /// </summary>
        triggerCount--;

        /// <summary>
        /// 只有当计数为 0 时，才执行重置
        /// </summary>
        if (triggerCount <= 0)
        {
            /// <summary>
            /// 确保计数不为负
            /// </summary>
            triggerCount = 0; 
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
