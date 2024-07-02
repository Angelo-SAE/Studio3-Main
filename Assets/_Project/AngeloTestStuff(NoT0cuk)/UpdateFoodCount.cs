using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateFoodCount : MonoBehaviour
{
    [SerializeField] private FoodItemObject foodItemList;
    private TMP_Text itemCount;

    private void Awake()
    {
      itemCount = GetComponent<TMP_Text>();
    }

    public void UpdateCount(int foodNumber)
    {
      itemCount.text = foodItemList.itemCount[foodNumber].ToString();
    }

    public void UpdateCountMinus(int foodNumber)
    {
      if(foodItemList.itemCount[foodNumber] > 0)
      {
        foodItemList.itemCount[foodNumber]--;
        itemCount.text = foodItemList.itemCount[foodNumber].ToString();
      }
    }
}
