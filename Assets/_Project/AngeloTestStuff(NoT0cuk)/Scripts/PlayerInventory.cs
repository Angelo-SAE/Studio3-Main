using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private IngredientObject playerInventory, ingredientList;
    [SerializeField] private UnityEvent NotifySlots;

    private void Awake()
    {
      playerInventory.ingredient = new Ingredient[4];
      playerInventory.count = 0;
    }

    public void AddToInv(int IngredientNumber)
    {
      if(playerInventory.count < 4 && ingredientList.ingredientCount[IngredientNumber] > 0)
      {
        for(int a = 0; a < 4; a++)
        {
          if(playerInventory.ingredient[a] is null)
          {
            ingredientList.ingredientCount[IngredientNumber]--;
            playerInventory.count++;
            playerInventory.ingredient[a] = ingredientList.ingredient[IngredientNumber];
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
      if(playerInventory.ingredient[slotNumber] is not null)
      {
        ingredientList.ingredientCount[playerInventory.ingredient[slotNumber].IngredientNumber]++;
        playerInventory.ingredient[slotNumber] = null;
        playerInventory.count--;
      }
    }
}
