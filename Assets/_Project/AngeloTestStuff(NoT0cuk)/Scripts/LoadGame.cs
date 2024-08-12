using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class LoadGame : MonoBehaviour
{

    [Header("Reset Values")]
    [SerializeField] private float defaultPlayerSpeed;

    [Header("New Day Values")]
    [SerializeField] private int newDay;
    [SerializeField] private int newMoney;
    [SerializeField] private int[] newIngredient;

    [SerializeField] private string[] saveInformation;
    private string path;


    [Header("Loading Scriptable Objects")]
    [SerializeField] private BoolObject loadingSave;
    [SerializeField] private IntObject day;
    [SerializeField] private FloatObject money;
    [SerializeField] private IngredientObject playerInventory;
    [SerializeField] private CharacterUpgradeObject characterUpgrade;
    [SerializeField] private CharacterUpgradeObject restaurantUpgrade;
    [SerializeField] private IngredientObject ingredientItemList;

    [Header("Extra Scriptable Objects")]
    [SerializeField] private FloatObject playerSpeed;
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private BoolObject gamePause, pause;

    [Header("Events")]
    [SerializeField] private UnityEvent onLoadSave, onNewDay;

    private void Awake()
    {
      path = Application.streamingAssetsPath + "/SavedGame/currentSave";
      playerSpeed.value = defaultPlayerSpeed;
      tablesAvailable.value = 0;
      characterUpgrade.inventorySize = 4;
      if(loadingSave.value && File.Exists(path))
      {
        LoadSave();
      } else {
        LoadNewGame();
      }
    }

    private void LoadNewGame()
    {
      NewDay();
      SetNewGameVariables();
      onLoadSave.Invoke();
    }

    private void SetNewGameVariables()
    {
      //day and money
      day.value = newDay;
      money.value = newMoney;

      //Player Inventory
      for(int a = 0; a < 6; a++)
      {
        playerInventory.ingredient[a] = null;
      }

      //Player Upgrades
      for(int a = 0; a < 4; a++)
      {
        characterUpgrade.characterUpgradeChecks[a] = false;
      }

      //Restaurant Upgrades
      for(int a = 0; a < 6; a++)
      {
        restaurantUpgrade.characterUpgradeChecks[a] = false;
      }

      //Ingredient Storage
      for(int a = 0; a < 11; a++)
      {
        ingredientItemList.ingredientCount[a] = newIngredient[a];
      }
    }

    private void LoadSave()
    {
      saveInformation = File.ReadAllLines(path);
      SetSaveVariables();
      NewDay();
      onLoadSave.Invoke();
    }

    private void SetSaveVariables()
    {
      //day and money
      day.value = GetIntFromSaveFile(0);
      money.value = GetIntFromSaveFile(1);

      //Player Inventory
      for(int a = 0; a < 6; a++)
      {
        if(GetIntFromSaveFile(a + 3) == 1)
        {
          playerInventory.ingredient[a] = ingredientItemList.ingredient[GetIntFromSaveFile(a + 10)];
        } else {
          playerInventory.ingredient[a] = null;
        }
      }

      //Player Upgrades
      for(int a = 0; a < 4; a++)
      {
        if(GetIntFromSaveFile(a + 17) == 1)
        {
          characterUpgrade.characterUpgradeChecks[a] = true;
        } else {
          characterUpgrade.characterUpgradeChecks[a] = false;
        }
      }

      //Restaurant Upgrades
      for(int a = 0; a < 6; a++)
      {
        if(GetIntFromSaveFile(a + 22) == 1)
        {
          restaurantUpgrade.characterUpgradeChecks[a] = true;
        } else {
          restaurantUpgrade.characterUpgradeChecks[a] = false;
        }
      }

      //Ingredient Storage
      for(int a = 0; a < 11; a++)
      {
        ingredientItemList.ingredientCount[a] = GetIntFromSaveFile(a + 29);
      }
    }

    private int GetIntFromSaveFile(int line)
    {
      string tempVariable = "";
      bool atInt = false;
      for(int a = 0; a < saveInformation[line].Length; a++)
      {
        if(atInt)
        {
          tempVariable = tempVariable + saveInformation[line][a];
        } else if(saveInformation[line][a] == ' ')
        {
          atInt = true;
        }
      }
      return int.Parse(tempVariable);
    }

    private void NewDay()
    {
      onNewDay.Invoke();
      gamePause.SetFalse();
      pause.SetFalse();
    }
}
