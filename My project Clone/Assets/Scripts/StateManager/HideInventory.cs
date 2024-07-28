using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInventory : MonoBehaviour
{
    public GameObject InventoryUI;
    bool isHide = true;
    public void Hide()
    {
        InventoryUI.SetActive(isHide);
        isHide = !isHide;
    }
}
