using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
//���ߣ����ƺ�
//����ʱ��: 2025��1��22��

/// <summary>
/// ����Ķ���������
/// ���⶯�����ֳ�ͻ
/// (����Ķ����л����ܻ���Ҫ�޸�)
/// </summary>
public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera; 
    /// <summary>
    /// ���� Cinemachine �������
    /// </summary>
    private float defaultZoom; 
    /// <summary>
    /// Ĭ�ϳ�ʼ����ֵ
    /// </summary>
    private float currentZoom; 
    /// <summary>
    /// ��ǰ���������ֵ
    /// </summary>
    private float forwardZoom; 
    /// <summary>
    /// ��һ������Ŀ��ֵ
    /// </summary>
    private int triggerCount = 0; 
    /// <summary>
    /// ����������ҽ���Ĵ���������
    /// </summary>

    private Tween zoomTween; 
    /// <summary>
    /// �������Ŷ���
    /// </summary>

    private void Start()
    {
        
        /// <summary>
        /// ��ʼ��Ĭ��ֵΪ��ǰ�����������С
        /// </summary>
        defaultZoom = cinemachineCamera.m_Lens.OrthographicSize;
        currentZoom = defaultZoom; 
        /// <summary>
        /// ��ʼʱ��ǰ����ֵ����Ĭ��ֵ
        /// </summary>
    }

    /// <summary>
    /// �������������ָ��Ŀ��ֵ
    /// </summary>
    /// <param name="forwardSize">Ŀ������ֵ������ֵ��</param>
    /// <param name="transitionTime">ƽ������ʱ��</param>
    public void AdjustZoom(float forwardSize, float transitionTime)
    {
        /// <summary>
        /// ���Ӵ���������
        /// </summary>
        triggerCount++;
 
        /// <summary>
        /// ����Ŀ������ֵ
        /// </summary>
        forwardZoom = forwardSize;

        /// <summary>
        /// ֹ֮ͣǰ�����Ŷ���������У�
        /// </summary>
        zoomTween?.Kill();

        /// <summary>
        /// �����µ����Ŷ���
        /// </summary>
        zoomTween = DOTween.To(
            () => cinemachineCamera.m_Lens.OrthographicSize,
            value => cinemachineCamera.m_Lens.OrthographicSize = value,
            forwardZoom,
            transitionTime
        ).OnComplete(() =>
        {
            /// <summary>
            /// ������ɺ󣬽���ǰ����ֵ����ΪĿ��ֵ
            /// </summary>
            currentZoom = forwardZoom;
        });
    }

    /// <summary>
    /// ����������ŵ�Ĭ��ֵ
    /// </summary>
    /// <param name="transitionTime">ƽ������ʱ��</param>
    public void ResetToDefault(float transitionTime)
    {
        /// <summary>
        /// ���ٴ���������
        /// </summary>
        triggerCount--;

        /// <summary>
        /// ֻ�е�����Ϊ 0 ʱ����ִ������
        /// </summary>
        if (triggerCount <= 0)
        {
            /// <summary>
            /// ȷ��������Ϊ��
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
