using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private IngredientObject playerInventory;
    [SerializeField] private int inventorySlot;
    [SerializeField] private Image slotSprite, backSprite;

    public void OnNotify()
    {
      if(playerInventory.ingredient[inventorySlot] is not null)
      {
        DisplayPlayerItem(playerInventory.ingredient[inventorySlot]);
      } else {
        DisplayNothing();
      }
    }

    public void EnableInventorySlot()
    {
      backSprite.color = new Color(1,1,1,1);
    }

    private void DisplayPlayerItem(Ingredient ingredient)
    {
      slotSprite.sprite = ingredient.IngredientSprite;
      slotSprite.color = new Color(1,1,1,1);
    }

    public void DisplayNothing()
    {
      slotSprite.sprite = null;
      slotSprite.color = new Color(1,1,1,0);
    }
}
