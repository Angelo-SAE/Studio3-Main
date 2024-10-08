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
    [SerializeField] private BoolObject paused;
    [SerializeField] private Button[] upgradeButtons, restaurantUpgradeButtons;
    [SerializeField] private UnityEvent OnPurchase, onCharacterUpgradePurchase, onRestaurantUpgradePurchase, onExit;
    private bool menuActive;

    private void Update()
    {
      if(menuActive)
      {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          CloseMarket();
        }
      }
    }

    public void ActivateMenu()
    {
      menuActive = true;
    }


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

    public void UpdateUpgradeButtons()
    {

      for(int a = 0; a < upgradeButtons.Length; a++)
      {
        if(characterUpgrade.characterUpgradeChecks[a])
        {
          upgradeButtons[a].interactable = false;
        }
      }

      for(int a = 0; a < restaurantUpgradeButtons.Length; a++)
      {
        if(restaurantUpgrade.characterUpgradeChecks[a])
        {
          restaurantUpgradeButtons[a].interactable = false;
        }
      }

    }

    public void CloseMarket()
    {
      menuActive = false;
      onExit.Invoke();
      Invoke("UnPauseGame", 0.1f);
    }

    private void UnPauseGame()
    {
      paused.SetFalse();
    }
}
