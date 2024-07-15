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
    [SerializeField] private BoolObject paused;
    [SerializeField] private CookingAssembly cookingAssembly;
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
        orderImage.color = new Color(1,1,1,1);
        orderNumber = orders.order[tableNumber].OrderNumber;
        ResetSprites();
        slotIngredientNumber = menu.orderIngredients[orders.order[tableNumber].OrderNumber].ingredientNumber;
        DisplayFoodGuide();
        AddItemToCooker();
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
        foodGuide[a].color = new Color(1,1,1,0.5f);
      }
    }

    private void ResetSprites()
    {
      for(int a = 0; a < orderIngredientCount; a++)
      {
        itemSlot[a].sprite = null;
        itemSlot[a].color = new Color(1,1,1,0);
        foodGuide[a].sprite = null;
        foodGuide[a].color = new Color(1,1,1,0);
      }
    }

    public void ResetOrder()
    {
      orderImage.sprite = null;
      orderImage.color = new Color(1,1,1,0);
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

    private void AddItemToCooker()
    {
      for(int a = 0; a < playerInventory.ingredient.Length; a++)
      {
        if(playerInventory.ingredient[a] is not null)
        {
          for(int b = 0; b < orderIngredientCount; b++)
          {
            if(slotIngredientNumber[b] == playerInventory.ingredient[a].IngredientNumber && cookerStorage.ingredient[b] is null)
            {
              tempIngredientCount++;
              itemSlot[b].sprite = playerInventory.ingredient[a].IngredientSprite;
              itemSlot[b].color = new Color(1,1,1,1);
              cookerStorage.ingredient[b] = playerInventory.ingredient[a];
              playerInventory.count--;
              playerInventory.ingredient[a] = null;
              break;
            }
          }
        }
      }
      onReturn.Invoke();
      if(orderIngredientCount == tempIngredientCount && orderIngredientCount != 0 && cooker.FoodHolder.transform.childCount == 0)
      {
        cookButton.interactable = true;
      }
    }

    public void Cook()
    {
      ClearCookerSlots();
      cooker.CookOrder(orderNumber);
      orders.cooked[tableNumber] = true;
      orders.changedOrder = true;
      ResetOrder();
      paused.value = false;
    }

    public void CookSlop()
    {
      ClearCookerSlots();
      ResetOrder();
      cooker.CookMush();
      paused.value = false;
    }

    public void StartAssemblyProcess()
    {
      gameObject.SetActive(false);
      cookingAssembly.PrepareAssembly();
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
