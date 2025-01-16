using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demo
{
    /// <summary>
    /// 灯光闪烁控制器
    /// 实现灯光强度的随机闪烁效果
    /// </summary>
    public class LightFork : MonoBehaviour
    {
        /// <summary>
        /// 灯光最小强度
        /// </summary>
        public float minIntensity = 0f;

        /// <summary>
        /// 灯光最大强度
        /// </summary>
        public float maxIntensity = 1f;

        /// <summary>
        /// 闪烁速度
        /// </summary>
        public float flickerSpeed = 0.07f;

        /// <summary>
        /// 灯光组件引用
        /// </summary>
        private UnityEngine.Rendering.Universal.Light2D lightSource;

        /// <summary>
        /// 随机值
        /// </summary>
        private float randomizer;

        /// <summary>
        /// 初始化组件引用
        /// </summary>
        private void Start()
        {
            lightSource = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }

        /// <summary>
        /// 更新灯光闪烁效果
        /// </summary>
        private void Update()
        {
            randomizer = Random.Range(0.0f, 1.0f);
            float noise = Mathf.PerlinNoise(randomizer, Time.time * flickerSpeed);
            lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }

}


