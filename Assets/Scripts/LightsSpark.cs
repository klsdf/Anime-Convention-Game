using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSpark : MonoBehaviour
{

    public float changeSpeed = 0.0001f; // 灯光变化速度
    private Light2D lightSource;


    private void Start()
    {
        lightSource = GetComponent<Light2D>();
    }

    private void Update()
    {
        // 计算新的强度值
        float newIntensity = lightSource.intensity + changeSpeed;

        // 如果新的强度值超过了最大值或最小值，反转变化的方向
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