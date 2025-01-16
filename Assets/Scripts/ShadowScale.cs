using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// 阴影缩放控制器
    /// 根据光源位置动态调整阴影大小和位置
    /// </summary>
    public class ShadowScale : MonoBehaviour
    {
        /// <summary>
        /// 光源变换组件
        /// </summary>
        public Transform lightSource;

        /// <summary>
        /// 阴影精灵渲染器
        /// </summary>
        private SpriteRenderer shadowSprite;

        /// <summary>
        /// 方向强度
        /// </summary>
        public float directionStrength = 1.0f;

        /// <summary>
        /// 基础X轴缩放值
        /// </summary>
        public float baseScaleX = 1.0f;

        /// <summary>
        /// 基础Y轴缩放值
        /// </summary>
        public float baseScaleY = 1.0f;

        /// <summary>
        /// 距离因子
        /// </summary>
        public float distanceFactor = 1.0f;

        /// <summary>
        /// 距离因子2
        /// </summary>
        public float distanceFactor2 = 0.035f;

        /// <summary>
        /// 初始化组件引用
        /// </summary>
        private void Awake()
        {
            shadowSprite = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// 更新阴影缩放和位置
        /// </summary>
        private void Update()
        {
            if(lightSource == null)
            {
                return;
            }

            // 计算方向向量
            Vector3 direction = (transform.position - lightSource.position).normalized * directionStrength;
            
            // 计算与光源的距离
            float distance = Vector2.Distance(transform.position, lightSource.position);

            // 计算缩放值
            float scaleX = Mathf.Clamp(Mathf.Abs((distance / distanceFactor) * direction.x), 0.0f, 2.0f);
            float scaleY = Mathf.Clamp(Mathf.Abs((distance / distanceFactor) * direction.y), 0.0f, 2.0f);

            // 应用基础缩放
            scaleX = baseScaleX + scaleX;
            scaleY = baseScaleY + scaleY;

            // 更新阴影变换
            transform.localScale = new Vector3(-scaleX, -scaleY, 1);
            transform.localPosition = direction * distance * distanceFactor2;
        }
    }
}


