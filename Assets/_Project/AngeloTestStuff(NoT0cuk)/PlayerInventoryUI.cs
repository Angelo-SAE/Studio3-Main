using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private FoodItemObject playerInventory;
    [SerializeField] private int inventorySlot;
    [SerializeField] private Image slotSprite;

    public void OnNotify()
    {
      if(playerInventory.foodItem[inventorySlot] is not null)
      {
        DisplayPlayerItem(playerInventory.foodItem[inventorySlot]);
      } else {
        DisplayNothing();
      }
    }

    private void DisplayPlayerItem(FoodItem item)
    {
      slotSprite.sprite = item.ItemSprite;
    }

    public void DisplayNothing()
    {
      slotSprite.sprite = null;
    }
}
