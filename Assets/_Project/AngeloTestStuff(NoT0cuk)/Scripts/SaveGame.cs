using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGame : MonoBehaviour
{
    private string path;
    [SerializeField] private string[] saveInformation;

    [Header("Scriptable Objects")]
    [SerializeField] private IntObject day;
    [SerializeField] private FloatObject money;
    [SerializeField] private IngredientObject playerInventory;
    [SerializeField] private CharacterUpgradeObject characterUpgrade;
    [SerializeField] private CharacterUpgradeObject restaurantUpgrade;
    [SerializeField] private IngredientObject ingredientItemList;


    private void Start()
    {
      path = Application.streamingAssetsPath + "/SavedGame/currentSave";
      //File.Exists(path); will return if the file exists put this at the start of the main menu to check if the player can load save or not
      //Use a boolobject to check if the player is loading a saave or not
    }

    public void SaveCurrentGame()
    {
      CreateSaveInformation();
      File.WriteAllLines(path, saveInformation);
    }

    private void CreateSaveInformation()
    {
      saveInformation = new string[40];

      //Day and money
      saveInformation[0] = "Day: " + day.value;
      saveInformation[1] = "Money: " + money.value;

      //Player Inventory
      for(int a = 0; a < 6; a++)
      {
        if(playerInventory.ingredient[a] is not null)
        {
          saveInformation[a + 3] = "PlaverInventory" + a + ": " + "1";
          saveInformation[a + 10] = "PlaverInventory" + a + ": " + playerInventory.ingredient[a].IngredientNumber;
        } else {
          saveInformation[a + 3] = "PlaverInventory" + a + ": " + "0";
          saveInformation[a + 10] = "PlaverInventory" + a + ": ";
        }
      }

      //Player Upgrades
      for(int a = 0; a < 4; a++)
      {
        if(characterUpgrade.characterUpgradeChecks[a])
        {
          saveInformation[a + 17] = "PlayerUpgrade" + a + ": " + "1";
        } else {
          saveInformation[a + 17] = "PlayerUpgrade" + a + ": " + "0";
        }
      }

      //Restaurant Upgrades
      for(int a = 0; a < 6; a++)
      {
        if(restaurantUpgrade.characterUpgradeChecks[a])
        {
          saveInformation[a + 22] = "RestaurantUpgrade" + a + ": " + "1";
        } else {
          saveInformation[a + 22] = "RestaurantUpgrade" + a + ": " + "0";
        }
      }

      //Ingredient Storage
      for(int a = 0; a < 11; a++)
      {
        saveInformation[a + 29] = "RestaurantUpgrade" + a + ": " + ingredientItemList.ingredientCount[a];
      }
    }
}
