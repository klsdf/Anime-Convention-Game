using UnityEngine;
using UnityEngine.UI;


public class SaveItemArea : MonoBehaviour
{

    private void Start()
    {

    }

    private void ClearAllSlots()
    {
        foreach (Transform child in transform)
        {
            // Debug.Log("找到子对象：" + child.name);
            child.GetComponent<Slot>().ClearItem();
        }
    }


}