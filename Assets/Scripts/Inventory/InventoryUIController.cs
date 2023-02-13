using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public InventoryBearer inventory;
    public List<GameObject> slots;

    void Update()
    {
        for (int i = 0; i < inventory.inventoryItems.Count; i++)
        {
            Item item = inventory.inventoryItems[i];
            GameObject slot = slots[i];

            Image img = slot.transform.GetChild(0).GetComponent<Image>();
            if (img != null)
            {
                img.sprite = item.itemData.icon;
                img.color = new Color(1, 1, 1, 1);
            }
            else Debug.Log("INV: Slot Image Was Null");

            TMPro.TextMeshProUGUI text = slot.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (text != null)
            {
                text.text = "x" + item.count;   
            }
            else Debug.Log("INV: Slot Text Was Null");
        }
    }
}
