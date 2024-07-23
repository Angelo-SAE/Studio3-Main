using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateIngredientCount : MonoBehaviour
{
    [SerializeField] private IngredientObject ingredientList;
    [SerializeField] private TMP_Text ingredientCount;

    public void UpdateCount(int ingredientNumber)
    {
      ingredientCount.text = ingredientList.ingredientCount[ingredientNumber].ToString();
    }

    public void UpdateCountMinus(int ingredientNumber)
    {
      if(ingredientList.ingredientCount[ingredientNumber] > 0)
      {
        ingredientList.ingredientCount[ingredientNumber]--;
        ingredientCount.text = ingredientList.ingredientCount[ingredientNumber].ToString();
      }
    }
}
