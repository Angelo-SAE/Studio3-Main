using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private FoodItemObject playerInventory, foodItemList;
    [SerializeField] private UnityEvent NotifySlots;

    private void Awake()
    {
      playerInventory.foodItem = new FoodItem[4];
      playerInventory.count = 0;
    }

    public void AddToInv(int foodNumber)
    {
      if(playerInventory.count < 4 && foodItemList.itemCount[foodNumber] > 0)
      {
        for(int a = 0; a < 4; a++)
        {
          if(playerInventory.foodItem[a] is null)
          {
            foodItemList.itemCount[foodNumber]--;
            playerInventory.count++;
            playerInventory.foodItem[a] = foodItemList.foodItem[foodNumber];
            NotifyInventorySlots();
            break;
          }
        }
      }
    }

    public void NotifyInventorySlots()
    {
      NotifySlots.Invoke();
    }

    public void ReturnItemToFridge(int slotNumber)
    {
      if(playerInventory.foodItem[slotNumber] is not null)
      {
        foodItemList.itemCount[playerInventory.foodItem[slotNumber].FoodNumber]++;
        playerInventory.foodItem[slotNumber] = null;
        playerInventory.count--;
      }
    }
}
