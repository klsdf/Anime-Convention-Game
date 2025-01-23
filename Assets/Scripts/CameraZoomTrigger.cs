using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraZoomTrigger : MonoBehaviour
{
    public CameraZoom cameraZoomController; // ���� CameraZoomController �ű�
    public float forwardZoom = 1f; // �����ڵ�Ŀ������ֵ
    public float transitionTime = 0.5f; // ����ƽ��ʱ��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������ʱ��������
            cameraZoomController.AdjustZoom(forwardZoom, transitionTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �뿪����ʱ����ΪĬ��ֵ
            cameraZoomController.ResetToDefault(transitionTime);
        }
    }
}
