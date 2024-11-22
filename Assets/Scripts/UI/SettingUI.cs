using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public bool isOpen = false;
    public Slider mainVolume;//������
    public Slider bgmVolume;//��������
    public Slider soundEffectVolume;//��Ч����
    public Button reduceMainBtn, addMainBtn;//������������ť
    public Button reduceBgmBtn, addBgmBtn;//��������������ť
    public Button reduceSundEffectBtn, addSundEffectBtn;//��Ч����������ť
    // Start is called before the first frame update
    void Start()
    {
        //�������������
        mainVolume.onValueChanged.AddListener(MainVelumeChange);
        reduceMainBtn.onClick.AddListener(() => { ChangeMainVelume(mainVolume, -0.1f); });
        addMainBtn.onClick.AddListener(() => { ChangeMainVelume(mainVolume, 0.1f); });
        //���������������
        bgmVolume.onValueChanged.AddListener(BGMVelumeChange);
        reduceBgmBtn.onClick.AddListener(() => { ChangeMainVelume(bgmVolume, -0.1f); });
        addBgmBtn.onClick.AddListener(() => { ChangeMainVelume(bgmVolume, 0.1f); });
        //��Ч�����������
        soundEffectVolume.onValueChanged.AddListener(SoundEffectVelumeChange);
        reduceSundEffectBtn.onClick.AddListener(() => { ChangeMainVelume(soundEffectVolume, -0.1f); });
        addSundEffectBtn.onClick.AddListener(() => { ChangeMainVelume(soundEffectVolume, 0.1f); });

        GameManager.Instance.SettingUI(this);
    }

    private void ChangeMainVelume(Slider velumeScrollbar, float value)
    {
        velumeScrollbar.value += value;
    }
    //����������
    private void MainVelumeChange(float volumeStrong)
    {
        GameManager.Instance.ChangeMainVolume(volumeStrong);
    }
    //������������
    private void BGMVelumeChange(float volumeStrong)
    {
        GameManager.Instance.ChangeBgmVolume(volumeStrong);
    }
    //��Ч��������
    private void SoundEffectVelumeChange(float volumeStrong)
    {
        GameManager.Instance.ChangeSoundEffectVolume(volumeStrong);
    }
    //�����ý��沢��ʼ��
    public void OpenSettingUI()
    {
        isOpen = true;
        this.gameObject.SetActive(true);
        GameManager.Instance.isDragingObject = true;
        mainVolume.value = GameManager.Instance.mainVolume;
        bgmVolume.value = GameManager.Instance.bgmVolume;
        soundEffectVolume.value = GameManager.Instance.soundEffectVolume;
    }
    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                isOpen = false;
                this.gameObject.SetActive(false);
                GameManager.Instance.isDragingObject = true;
            }
        }
    }
}
