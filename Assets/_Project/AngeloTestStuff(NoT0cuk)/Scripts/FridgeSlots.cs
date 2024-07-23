using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FridgeSlots : MonoBehaviour
{
    [SerializeField] private IngredientObject ingredientItemList;
    [SerializeField] private UnityEvent UpdateSlotCount;

    public void UpdateSlots()
    {
      UpdateSlotCount.Invoke();
    }
}
