using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUpgradeManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private CharacterUpgradeObject characterUpgrade;
    [SerializeField] private FloatObject playerSpeed;
    [Header("Upgrades")]
    [SerializeField] private float speedIncreaseOne;
    [SerializeField] private float speedIncreaseTwo;
    [SerializeField] private UnityEvent enableSlotOne, enableSlotTwo;

    private bool speedOne, speedTwo, inventoryOne, inventoryTwo;

    public void CheckForUpgrades()
    {
      if(characterUpgrade.characterUpgradeChecks[0] && !speedOne)
      {
        speedOne = true;
        UpdatePlayerSpeed(speedIncreaseOne);
      }
      if(characterUpgrade.characterUpgradeChecks[1] && !speedTwo)
      {
        speedTwo = true;
        UpdatePlayerSpeed(speedIncreaseTwo);
      }
      if(characterUpgrade.characterUpgradeChecks[2] && !inventoryOne && !inventoryTwo)
      {
        inventoryOne = true;
        enableSlotOne.Invoke();
        characterUpgrade.inventorySize++;
      }
      if(characterUpgrade.characterUpgradeChecks[3] && !inventoryTwo)
      {
        inventoryTwo = true;
        enableSlotTwo.Invoke();
        characterUpgrade.inventorySize++;
      }
    }

    private void UpdatePlayerSpeed(float speedIncrease)
    {
      playerSpeed.value += speedIncrease;
    }
}
