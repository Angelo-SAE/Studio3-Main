using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Market : MonoBehaviour
{
    [SerializeField] private IngredientObject ingredientItemList;
    [SerializeField] private FloatObject money;
    [SerializeField] private float[] ingredientPrices;
    [SerializeField] private UnityEvent OnPurchase;


    public void PurchaseIngredient(int ingredientNumber)
    {
      if(ingredientPrices[ingredientNumber] < money.value)
      {
        money.value -= ingredientPrices[ingredientNumber];
        ingredientItemList.ingredientCount[ingredientNumber]++;
        OnPurchase.Invoke();
      }
    }
}
