//作者：闫辰祥
//创建时间: 2024年12月7日

using UnityEngine;

public enum PanelType
{
    Assert,
    Item,
    Pause,
    Setting
}



/// <summary>
/// UI控制器，管理和统筹所有的UI面板
/// </summary>
public class UIController : Singleton<UIController>
{

    
    public GameObject assertPanel;
    public GameObject itemPanel;
    public GameObject pausePanel;

    public GameObject settingPanel;

    public void ShowPanel(PanelType panelType)
    {
        assertPanel.SetActive(panelType == PanelType.Assert);
        itemPanel.SetActive(panelType == PanelType.Item);
        pausePanel.SetActive(panelType == PanelType.Pause);
        settingPanel.SetActive(panelType == PanelType.Setting);
        AudioPlayerController.Instance.PlayAudio(SoundType.ButtonSE);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPanel(PanelType.Pause);
        }
    }

}