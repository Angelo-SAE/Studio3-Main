using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cooker : Interactable
{
    [SerializeField] private FoodItemObject playerInventory;
    [SerializeField] private GameObject lettuceSandwich, platePosition;
    [SerializeField] private UnityEvent afterCheck;
    private int breadSlot, lettuceSlot;
    private bool hasBread, hasLettuce;

    public override void Interact()
    {
      CheckForIngredients();
    }

    private void CheckForIngredients()
    {
      for(int a = 0; a < 4; a++)
      {
        if(playerInventory.foodItem[a] is not null)
        {
          if(playerInventory.foodItem[a].FoodNumber == 0 && !hasBread)
          {
            hasBread = true;
            breadSlot = a;
          }
          if(playerInventory.foodItem[a].FoodNumber == 1 && !hasLettuce)
          {
            hasLettuce = true;
            lettuceSlot = a;
          }
        }
      }
      if(hasBread && hasLettuce)
      {
        CookSandwich();
      }
    }

    private void CookSandwich()
    {
      playerInventory.foodItem[breadSlot] = null;
      playerInventory.foodItem[lettuceSlot] = null;
      playerInventory.count -= 2;
      afterCheck.Invoke();
      Instantiate(lettuceSandwich, platePosition.transform);
      hasBread = false;
      hasLettuce = false;
    }
}
