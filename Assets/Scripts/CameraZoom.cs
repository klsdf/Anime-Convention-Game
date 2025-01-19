using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera; // ���� Cinemachine �������
    private float defaultZoom; // Ĭ�ϳ�ʼ����ֵ
    private float currentZoom; // ��ǰ���������ֵ
    private float forwardZoom; // ��һ������Ŀ��ֵ
    private int triggerCount = 0; // ����������ҽ���Ĵ���������

    private Tween zoomTween; // �������Ŷ���

    private void Start()
    {
        // ��ʼ��Ĭ��ֵΪ��ǰ�����������С
        defaultZoom = cinemachineCamera.m_Lens.OrthographicSize;
        currentZoom = defaultZoom; // ��ʼʱ��ǰ����ֵ����Ĭ��ֵ
    }

    /// <summary>
    /// �������������ָ��Ŀ��ֵ
    /// </summary>
    /// <param name="forwardSize">Ŀ������ֵ������ֵ��</param>
    /// <param name="transitionTime">ƽ������ʱ��</param>
    public void AdjustZoom(float forwardSize, float transitionTime)
    {
        // ���Ӵ���������
        triggerCount++;

        // ����Ŀ������ֵ
        forwardZoom = forwardSize;

        // ֹ֮ͣǰ�����Ŷ���������У�
        zoomTween?.Kill();

        // �����µ����Ŷ���
        zoomTween = DOTween.To(
            () => cinemachineCamera.m_Lens.OrthographicSize,
            value => cinemachineCamera.m_Lens.OrthographicSize = value,
            forwardZoom,
            transitionTime
        ).OnComplete(() =>
        {
            // ������ɺ󣬽���ǰ����ֵ����ΪĿ��ֵ
            currentZoom = forwardZoom;
        });
    }

    /// <summary>
    /// ����������ŵ�Ĭ��ֵ
    /// </summary>
    /// <param name="transitionTime">ƽ������ʱ��</param>
    public void ResetToDefault(float transitionTime)
    {
        // ���ٴ���������
        triggerCount--;

        // ֻ�е�����Ϊ 0 ʱ����ִ������
        if (triggerCount <= 0)
        {
            triggerCount = 0; // ȷ��������Ϊ��
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
