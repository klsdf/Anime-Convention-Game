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

//��ά��ʶ�����ɿ�����
public class QRCode : MonoBehaviour
{
    #region ɨ���ά��
    //����һ�����ڴ洢���õ��Ի��ֻ�����ͷ�����RawImage
    public RawImage m_cameraTexture;

    //����ͷʵʱ��ʾ�Ļ���
    private WebCamTexture m_webCameraTexture;

    //����һ����ȡ��ά��ı���
    private BarcodeReader m_barcodeRender = new BarcodeReader();

    //��ü���һ�ζ�ά��
    private float m_delayTime = 3f;

    private Quaternion initialRotation = Quaternion.identity; // ��ʼ��ת
    #endregion

    #region ���ɶ�ά��
    //������ʾ���ɵĶ�ά��RawImage
    public RawImage m_QRCode;

    //����һ��д��ά��ı���
    private BarcodeWriter m_barcodeWriter;
    #endregion
    //public Button openBtn;
    public TextMeshProUGUI ShiBeiResult;
    //public Button closeBtn;
    #region ɨ���ά��
    void Start()
    {
        //openBtn.onClick.AddListener(OpenWebCam);
        //closeBtn.onClick.AddListener(closeWebCam);

        initialRotation = GetRotationForOrientation(Screen.orientation);
        m_cameraTexture.transform.rotation = initialRotation;
        //��������ͷ����������ʾ����ĻRawImage��
        WebCamDevice[] tDevices = WebCamTexture.devices;    //��ȡ��������ͷ
        string tDeviceName = tDevices[0].name;  //��ȡ��һ������ͷ���õ�һ������ͷ�Ļ�������ͼƬ��Ϣ
        m_webCameraTexture = new WebCamTexture(tDeviceName, 400, 300);//����,��,��
        m_cameraTexture.texture = m_webCameraTexture;   //��ֵͼƬ��Ϣ
        m_webCameraTexture.Play();  //��ʼʵʱ��ʾ

        InvokeRepeating("CheckQRCode", 0, m_delayTime);
    }
    Quaternion GetRotationForOrientation(ScreenOrientation orientation)
    {
        switch (orientation)
        {
            case ScreenOrientation.Portrait:
                return Quaternion.Euler(0, 0, -90); // ����ʱ��ת90��
            case ScreenOrientation.PortraitUpsideDown:
                return Quaternion.Euler(0, 0, 90); // ��������ʱ��ת-90��
            case ScreenOrientation.LandscapeLeft:
                return Quaternion.Euler(0, 0, 0); // ������ʱ���ֲ���
            case ScreenOrientation.LandscapeRight:
                return Quaternion.Euler(0, 0, 0); // ������ʱ���ֲ���
            default:
                return Quaternion.identity; // ����������ֳ�ʼ��ת
        }
    }
    /// <summary>
    /// ������ά�뷽��
    /// </summary>
    void CheckQRCode()
    {
        Debug.Log("��ʼʶ���ά��");
        ShiBeiResult.text = "��ʼʶ���ά��";
        //�洢����ͷ������Ϣ��ͼת������ɫ����
        Color32[] m_colorData = m_webCameraTexture.GetPixels32();

        //�������еĶ�ά����Ϣ��������
        var tResult = m_barcodeRender.Decode(m_colorData, m_webCameraTexture.width, m_webCameraTexture.height);

        if (tResult != null)
        {
            Debug.Log(tResult.Text);
            ShiBeiResult.text = tResult.Text;
        }
    }
    #endregion

    #region �����ַ������ɶ�ά��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //������д����  ��߱���256  ���򱨴�
            //ShowQRCode("ħ������", 256, 256);
            OpenWebCam();
        }
        // �����Ļ����仯
        if (Screen.orientation != ScreenOrientation.AutoRotation)
        {
            // �����Ļ����ı䣬�������ת
            Quaternion newRotation = GetRotationForOrientation(Screen.orientation);
            m_cameraTexture.transform.rotation = newRotation;
        }
    }

    /// <summary>
    /// ��ʾ���ƵĶ�ά��
    /// </summary>
    /// <param name="s_formatStr">ɨ����Ϣ</param>
    /// <param name="s_width">���</param>
    /// <param name="s_height">���</param>
    void ShowQRCode(string s_str, int s_width, int s_height)
    {
        //����Texture2D�������s
        Texture2D tTexture = new Texture2D(s_width, s_height);

        //�������Ӧ����ͼ����
        tTexture.SetPixels32(GeneQRCode(s_str, s_width, s_height));

        tTexture.Apply();

        //��ֵ��ͼ
        m_QRCode.texture = tTexture;
        //������д����  ��߱���256  ���򱨴�
        Debug.Log("��ʼ�ֶ�ʶ���ά��");
        //�洢����ͷ������Ϣ��ͼת������ɫ����
        Color32[] m_colorData = tTexture.GetPixels32();

        //�������еĶ�ά����Ϣ��������
        var tResult = m_barcodeRender.Decode(m_colorData, s_width, s_height);

        if (tResult != null)
        {
            Debug.Log(tResult.Text);
        }
    }

    /// <summary>
    /// ���ض�Ӧ��ɫ����
    /// </summary>
    /// <param name="s_formatStr">ɨ����Ϣ</param>
    /// <param name="s_width">���</param>
    /// <param name="s_height">���</param>
    Color32[] GeneQRCode(string s_formatStr, int s_width, int s_height)
    {
        //�������ı����ʽ���������Ĳ�֧��
        QrCodeEncodingOptions tOptions = new QrCodeEncodingOptions();
        tOptions.CharacterSet = "UTF-8";
        //���ÿ��
        tOptions.Width = s_width;
        tOptions.Height = s_height;
        //���ö�ά������Ե�Ŀհ׾���
        tOptions.Margin = 3;

        //��������д��ά�������       (����Ϊ�����ʽ����ά�롢������...��    �����ʽ��֧�ֵı����ʽ��    )
        m_barcodeWriter = new BarcodeWriter { Format = BarcodeFormat.QR_CODE, Options = tOptions };

        //��������Ҫ��������������Ϣ��ֵ��
        return m_barcodeWriter.Write(s_formatStr);
    }

    public void OpenWebCam()
    {
        Debug.Log("��");
        m_webCameraTexture.Play();
        InvokeRepeating("CheckQRCode", 0, m_delayTime);
    }

    public void closeWebCam()
    {
        Debug.Log("�ر�");
        m_webCameraTexture.Stop();
        CancelInvoke("CheckQRCode");
        SceneManager.LoadScene("StartScene");
    }
    #endregion
}