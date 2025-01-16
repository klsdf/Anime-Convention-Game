using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 灯光渐变控制器
/// 实现灯光强度的平滑渐变效果
/// </summary>
public class LightSpark : MonoBehaviour
{
    /// <summary>
    /// 灯光变化速度
    /// </summary>
    public float changeSpeed = 0.0001f;

    /// <summary>
    /// 灯光组件引用
    /// </summary>
    private Light2D lightSource;

    /// <summary>
    /// 初始化组件引用
    /// </summary>
    private void Start()
    {
        lightSource = GetComponent<Light2D>();
    }

    /// <summary>
    /// 更新灯光强度
    /// </summary>
    private void Update()
    {
        float newIntensity = lightSource.intensity + changeSpeed;

        if (newIntensity > 1f || newIntensity < 0.3f)
        {
            changeSpeed = -changeSpeed;
        }
        else
        {
            lightSource.intensity = newIntensity;
        }
    }
}