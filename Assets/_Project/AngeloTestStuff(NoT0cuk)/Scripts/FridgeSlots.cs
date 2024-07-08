using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FridgeSlots : MonoBehaviour
{
    [SerializeField] private IngredientObject ingredientItemList;
    [SerializeField] private UnityEvent UpdateSlotCount;

    private void Awake()
    {
      ingredientItemList.ingredientCount[0] = 10;
      ingredientItemList.ingredientCount[1] = 10;
      ingredientItemList.ingredientCount[2] = 10;
      ingredientItemList.ingredientCount[3] = 10;
      ingredientItemList.ingredientCount[4] = 10;
      ingredientItemList.ingredientCount[5] = 10;
      ingredientItemList.ingredientCount[6] = 10;
    }

    public void UpdateSlots()
    {
      UpdateSlotCount.Invoke();
    }
}
