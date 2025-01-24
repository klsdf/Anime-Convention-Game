using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ߣ����ƺ�
//����ʱ��: 2025��1��22��

/// <summary>
/// �������������
/// ��������Ĵ�С�Ͷ���ʱ��
/// </summary>
public class CameraZoomTrigger : MonoBehaviour
{
    public CameraZoom cameraZoomController; 
    /// <summary>
    /// ���� CameraZoomController �ű�
    /// </summary>
    public float forwardZoom = 1f; 
    /// <summary>
    /// �����ڵ�Ŀ������ֵ
    /// </summary>
    public float transitionTime = 0.5f; 
    /// <summary>
    /// ����ƽ��ʱ��
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            /// <summary>
            /// ��������ʱ��������
            /// </summary>
            cameraZoomController.AdjustZoom(forwardZoom, transitionTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            /// <summary>
            /// �뿪����ʱ����ΪĬ��ֵ
            /// </summary>
            cameraZoomController.ResetToDefault(transitionTime);
        }
    }
}
