using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public List<SlotUI> slotUIlist;
    private GameObject parentUI;
    void Awake()
    {
        parentUI = transform.Find("ParentUI").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnClose();
        }

    }
    void Init()
    {
        slotUIlist = new List<SlotUI>(new SlotUI[21]);

        SlotUI[] slotUIArray = transform.GetComponentsInChildren<SlotUI>();

        foreach (SlotUI slotUI in slotUIArray)
        {
            slotUIlist[slotUI.index] = slotUI;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        List<SlotData> slotdatalist = InventoryManager.Instance.backpack.slotList;

        for (int i = 0; i < slotdatalist.Count; i++)
        {
            slotUIlist[i].SetData(slotdatalist[i]);
        }
    }

    public void OnClose()
    {
        parentUI.SetActive(!parentUI.activeSelf);
    }

    public void MouseOnClick()
    {
        OnClose();
    }
     
}
