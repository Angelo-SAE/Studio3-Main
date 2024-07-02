using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FridgeSlots : MonoBehaviour
{
    [SerializeField] private FoodItemObject foodItemList;
    [SerializeField] private UnityEvent UpdateSlotCount;

    private void Awake()
    {
      foodItemList.itemCount[0] = 4;
      foodItemList.itemCount[1] = 4;
    }

    public void UpdateSlots()
    {
      UpdateSlotCount.Invoke();
    }
}
