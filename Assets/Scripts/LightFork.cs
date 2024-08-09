using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demo
{
    public class LightFork : MonoBehaviour
    {
        public float minIntensity = 0f; // 灯光的最小强度
        public float maxIntensity = 1f; // 灯光的最大强度
        public float flickerSpeed = 0.07f; // 灯光闪烁的速度

        private UnityEngine.Rendering.Universal.Light2D lightSource; 
        private float randomizer; 

        void Start()
        {
            lightSource = GetComponent<UnityEngine.Rendering.Universal.Light2D>(); // 获取灯光组件
        }

        void Update()
        {
            // 使用Perlin噪声函数来随机化闪烁的强度
            randomizer = Random.Range(0.0f, 1.0f);
            float noise = Mathf.PerlinNoise(randomizer, Time.time * flickerSpeed);
            lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }

}


