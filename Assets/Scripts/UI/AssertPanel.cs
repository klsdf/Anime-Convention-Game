//作者：闫辰祥
//创建时间: 2024年12月8日

using UnityEngine;
using UnityEngine.UI;

public class AssertPanel : MonoBehaviour
{
    public Button closeBtn;

    private void Awake() {
        closeBtn.onClick.AddListener(CloseAssertPanel);
    }

    private void CloseAssertPanel()
    {
        UIController.Instance.ShowPanel(PanelType.Item);
    }

}