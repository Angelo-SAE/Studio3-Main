using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    [SerializeField] private IngredientObject ingredientItemList;
    [SerializeField] private CharacterUpgradeObject characterUpgrade, restaurantUpgrade;
    [SerializeField] private FloatObject money, moneySpent;
    [SerializeField] private Button[] upgradeButtons, restaurantUpgradeButtons;
    [SerializeField] private UnityEvent OnPurchase, onCharacterUpgradePurchase, onRestaurantUpgradePurchase;


    public void PurchaseIngredient(int ingredientNumber)
    {
      if(ingredientItemList.ingredientPrice[ingredientNumber] <= money.value)
      {
        money.value -= ingredientItemList.ingredientPrice[ingredientNumber];
        moneySpent.value += ingredientItemList.ingredientPrice[ingredientNumber];
        ingredientItemList.ingredientCount[ingredientNumber]++;
        OnPurchase.Invoke();
      }
    }

    public void PurchaseUpgrade(int upgradeNumber)
    {
      if(characterUpgrade.characterUpgradePrice[upgradeNumber] <= money.value)
      {
        upgradeButtons[upgradeNumber].interactable = false;
        money.value -= characterUpgrade.characterUpgradePrice[upgradeNumber];
        moneySpent.value += characterUpgrade.characterUpgradePrice[upgradeNumber];
        characterUpgrade.characterUpgradeChecks[upgradeNumber] = true;
        onCharacterUpgradePurchase.Invoke();
      }
    }

    public void PurchaseRestaurantUpgrade(int upgradeNumber)
    {
      if(restaurantUpgrade.characterUpgradePrice[upgradeNumber] <= money.value)
      {
        restaurantUpgradeButtons[upgradeNumber].interactable = false;
        money.value -= restaurantUpgrade.characterUpgradePrice[upgradeNumber];
        moneySpent.value += restaurantUpgrade.characterUpgradePrice[upgradeNumber];
        restaurantUpgrade.characterUpgradeChecks[upgradeNumber] = true;
        onRestaurantUpgradePurchase.Invoke();
      }
    }
}
