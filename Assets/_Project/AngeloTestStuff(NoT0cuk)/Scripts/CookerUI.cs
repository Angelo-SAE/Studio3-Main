using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CookerUI : MonoBehaviour
{
    [SerializeField] private MenuObject menu;
    [SerializeField] private OrderObject orders;
    [SerializeField] private IngredientObject cookerStorage, ingredientList, playerInventory;
    [SerializeField] private Cooker cooker;
    [SerializeField] private Button cookButton;
    [SerializeField] private Image orderImage;
    [SerializeField] private Image[] foodGuide, itemSlot;
    [SerializeField] private UnityEvent onReturn;
    private int orderNumber, orderIngredientCount, tempIngredientCount, tableNumber;
    private int[] slotIngredientNumber;

    private void Awake()
    {
      cookerStorage.ingredient = new Ingredient[itemSlot.Length];
      cookButton.interactable = false;
    }

    public void SelectOrder(int table)
    {
      tableNumber = table;
      if(orders.order[tableNumber] is not null)
      {
        ReturnItemsToInventory();
        orderImage.sprite = orders.order[tableNumber].OrderSprite;
        orderNumber = orders.order[tableNumber].OrderNumber;
        ResetSprites();
        slotIngredientNumber = menu.orderIngredients[orders.order[tableNumber].OrderNumber].ingredientNumber;
        DisplayFoodGuide();
      }
    }

    private void DisplayFoodGuide()
    {
      orderIngredientCount = slotIngredientNumber.Length;
      tempIngredientCount = 0;
      cookButton.interactable = false;
      for(int a = 0; a < slotIngredientNumber.Length; a++)
      {
        foodGuide[a].sprite = ingredientList.ingredient[slotIngredientNumber[a]].IngredientSprite;
      }
    }

    private void ResetSprites()
    {
      for(int a = 0; a < orderIngredientCount; a++)
      {
        itemSlot[a].sprite = null;
        foodGuide[a].sprite = null;
      }
    }

    public void ResetOrder()
    {
      orderImage.sprite = null;
      slotIngredientNumber = null;
      orderIngredientCount = 0;
      cookButton.interactable = false;
    }

    public void ReturnItemsToInventory()
    {
      for(int a = 0; a < orderIngredientCount; a++)
      {
        if(cookerStorage.ingredient[a] is not null)
        {
          for(int b = 0; b < 4; b++)
          {
            if(playerInventory.ingredient[b] is null)
            {
              playerInventory.count++;
              playerInventory.ingredient[b] = cookerStorage.ingredient[a];
              cookerStorage.ingredient[a] = null;
              break;
            }
          }
        }
      }
      ResetSprites();
      onReturn.Invoke();
    }

    public void AddItemToCooker(int slotNumber)
    {
      if(playerInventory.ingredient[slotNumber] is not null)
      {
        for(int a = 0; a < orderIngredientCount; a++)
        {
          if(slotIngredientNumber[a] == playerInventory.ingredient[slotNumber].IngredientNumber && cookerStorage.ingredient[a] is null)
          {
            tempIngredientCount++;
            itemSlot[a].sprite = playerInventory.ingredient[slotNumber].IngredientSprite;
            cookerStorage.ingredient[a] = playerInventory.ingredient[slotNumber];
            playerInventory.count--;
            playerInventory.ingredient[slotNumber] = null;
            break;
          }
        }
      }
      if(orderIngredientCount == tempIngredientCount && orderIngredientCount != 0)
      {
        cookButton.interactable = true;
      }
    }

    public void Cook()
    {
      ClearCookerSlots();
      cooker.CookOrder(orderNumber);
      orders.order[tableNumber] = null;
      orders.changedOrder = true;
      ResetOrder();
    }

    private void ClearCookerSlots()
    {
      for(int a = 0; a < orderIngredientCount; a++)
      {
        cookerStorage.ingredient[a] = null;
      }
      ResetSprites();
    }
}
