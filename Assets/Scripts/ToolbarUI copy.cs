using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{

    public List<Toolbar> slotUIlist;
    private Toolbar selcetedtoolbar;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void Update()
    {
        for(int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                if (selcetedtoolbar != null)
                {
                    selcetedtoolbar.Unhighlight();
                }

                int index = i - (int)KeyCode.Alpha1;
                selcetedtoolbar = slotUIlist[index];
                selcetedtoolbar.Highlight();


            }
            
        }              
    }
    void Init()
    {
        slotUIlist = new List<Toolbar>(new Toolbar[9]);

        Toolbar[] slotUIArray = transform.GetComponentsInChildren<Toolbar>();

        foreach (Toolbar slotUI in slotUIArray)
        {
            slotUIlist[slotUI.index] = slotUI;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        List<SlotData> slotdatalist = InventoryManager.Instance.toolbarData.slotList;

        for (int i = 0; i < slotdatalist.Count; i++)
        {
            slotUIlist[i].SetData(slotdatalist[i]);
        }
    }
}
