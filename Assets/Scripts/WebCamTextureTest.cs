/*************************************************
 * ��Ŀ���ƣ�Unityʵ����������ͷɨ�������ɶ�ά��
 * �ű������ˣ�ħ��
 * �ű�����ʱ�䣺2017.12.20
 * �ű����ܣ���ά��ʶ�����ɿ�����
 * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

/// <summary>
/// 摄像头纹理测试控制器
/// 实现摄像头扫描和二维码识别功能
/// </summary>
public class WebCamTextureTest : MonoBehaviour
{
    /// <summary>
    /// 摄像头图像显示组件
    /// </summary>
    public RawImage cameraTextureDisplay;

    /// <summary>
    /// 摄像头实时画面纹理
    /// </summary>
    private WebCamTexture webCamTexture;

    /// <summary>
    /// 二维码解码器
    /// </summary>
    private BarcodeReader barcodeReader = new BarcodeReader();

    /// <summary>
    /// 扫描间隔时间
    /// </summary>
    private float scanInterval = 3f;

    /// <summary>
    /// 初始旋转角度
    /// </summary>
    private Quaternion initialRotation = Quaternion.identity;

    /// <summary>
    /// 生成的二维码显示组件
    /// </summary>
    public RawImage qrCodeDisplay;

    /// <summary>
    /// 二维码生成器
    /// </summary>
    private BarcodeWriter barcodeWriter;

    /// <summary>
    /// 识别结果文本显示
    /// </summary>
    public TextMeshProUGUI scanResultText;

    /// <summary>
    /// 初始化摄像头和二维码扫描
    /// </summary>
    private void Start()
    {
        // 设置初始旋转角度
        initialRotation = GetRotationForOrientation(Screen.orientation);
        cameraTextureDisplay.transform.rotation = initialRotation;

        // 初始化摄像头
        WebCamDevice[] devices = WebCamTexture.devices;    // 获取所有摄像头
        if (devices.Length == 0)
        {
            Debug.LogError("No webcam devices found!");
            return;
        }

        string deviceName = devices[0].name;  // 获取第一个摄像头
        webCamTexture = new WebCamTexture(deviceName, 400, 300); // 宽度,高度
        cameraTextureDisplay.texture = webCamTexture;   // 设置图像显示
        webCamTexture.Play();  // 开始实时显示

        // 开始定期检测二维码
        InvokeRepeating(nameof(ScanQRCode), 0, scanInterval);
    }

    /// <summary>
    /// 获取屏幕方向对应的旋转角度
    /// </summary>
    /// <param name="orientation">屏幕方向</param>
    /// <returns>对应的旋转四元数</returns>
    private Quaternion GetRotationForOrientation(ScreenOrientation orientation)
    {
        switch (orientation)
        {
            case ScreenOrientation.LandscapeLeft:
                return Quaternion.Euler(0, 0, 90);
            case ScreenOrientation.LandscapeRight:
                return Quaternion.Euler(0, 0, -90);
            case ScreenOrientation.PortraitUpsideDown:
                return Quaternion.Euler(0, 0, 180);
            case ScreenOrientation.Portrait:
            default:
                return Quaternion.identity;
        }
    }

    /// <summary>
    /// 扫描并识别二维码
    /// </summary>
    private void ScanQRCode()
    {
        Debug.Log("开始识别二维码");
        scanResultText.text = "开始识别二维码";

        Color32[] pixels = webCamTexture.GetPixels32();
        var result = barcodeReader.Decode(pixels, webCamTexture.width, webCamTexture.height);

        if (result != null)
        {
            Debug.Log(result.Text);
            scanResultText.text = result.Text;
        }
    }
}