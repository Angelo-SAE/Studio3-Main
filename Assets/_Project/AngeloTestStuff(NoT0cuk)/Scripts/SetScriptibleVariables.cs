using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetScriptibleVariables : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int dayDefault;
    [SerializeField] private float defaultPlayerSpeed, startingMoney;


    [Header("Scriptable Objects")]
    [SerializeField] private IntObject day;
    [SerializeField] private FloatObject playerSpeed, money;
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private IngredientObject ingredientItemList;
    [SerializeField] private CharacterUpgradeObject characterUpgrade, restaurantUpgrade;
    [SerializeField] private UnityEvent OnAwake;

    private void Awake()
    {
      day.value = dayDefault;
      playerSpeed.value = defaultPlayerSpeed;
      tablesAvailable.value = 0;
      money.value = startingMoney;

      ingredientItemList.ingredientCount[0] = 5;
      ingredientItemList.ingredientCount[1] = 5;
      ingredientItemList.ingredientCount[2] = 5;
      ingredientItemList.ingredientCount[3] = 5;
      ingredientItemList.ingredientCount[4] = 5;
      ingredientItemList.ingredientCount[5] = 5;
      ingredientItemList.ingredientCount[6] = 5;
      ingredientItemList.ingredientCount[7] = 5;
      ingredientItemList.ingredientCount[8] = 5;
      ingredientItemList.ingredientCount[9] = 5;
      ingredientItemList.ingredientCount[10] = 5;

      characterUpgrade.characterUpgradeChecks[0] = false;
      characterUpgrade.characterUpgradeChecks[1] = false;
      characterUpgrade.characterUpgradeChecks[2] = false;
      characterUpgrade.characterUpgradeChecks[3] = false;
      characterUpgrade.inventorySize = 4;

      restaurantUpgrade.characterUpgradeChecks[0] = false;
      restaurantUpgrade.characterUpgradeChecks[1] = false;
      restaurantUpgrade.characterUpgradeChecks[2] = false;
      restaurantUpgrade.characterUpgradeChecks[3] = false;
      restaurantUpgrade.characterUpgradeChecks[4] = false;
      restaurantUpgrade.characterUpgradeChecks[5] = false;

    }

    private void Start()
    {
      OnAwake.Invoke();
    }
}
