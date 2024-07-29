using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private IngredientObject playerInventory, ingredientList;
    [SerializeField] private UnityEvent NotifySlots;
    [SerializeField] private Image[] fridgeItemSlots;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration;

    private bool[] isFlashing;

    private void Awake()
    {
        playerInventory.ingredient = new Ingredient[4];
        playerInventory.count = 0;
        isFlashing = new bool[fridgeItemSlots.Length];
    }

    public void AddToInv(int IngredientNumber)
    {
        if (playerInventory.count < 4 && ingredientList.ingredientCount[IngredientNumber] > 0)
        {
            for (int a = 0; a < 4; a++)
            {
                if (playerInventory.ingredient[a] is null)
                {
                    ingredientList.ingredientCount[IngredientNumber]--;
                    playerInventory.count++;
                    playerInventory.ingredient[a] = ingredientList.ingredient[IngredientNumber];
                    NotifyInventorySlots();
                    return;
                }
            }
        }
        else if (playerInventory.count >= 4)
        {
            {
                
                if (!isFlashing[IngredientNumber])
                {
                    StartCoroutine(FlashFridgeSlot(IngredientNumber));
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
        if (playerInventory.ingredient[slotNumber] is not null)
        {
            ingredientList.ingredientCount[playerInventory.ingredient[slotNumber].IngredientNumber]++;
            playerInventory.ingredient[slotNumber] = null;
            playerInventory.count--;
        }
    }

    private IEnumerator FlashFridgeSlot(int IngredientNumber)
    {
        isFlashing[IngredientNumber] = true;
        Color originalColor = fridgeItemSlots[IngredientNumber].color;
        fridgeItemSlots[IngredientNumber].color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        fridgeItemSlots[IngredientNumber].color = originalColor;
        isFlashing[IngredientNumber] = false;
    }
}
