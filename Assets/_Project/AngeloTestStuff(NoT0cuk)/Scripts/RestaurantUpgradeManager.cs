using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestaurantUpgradeManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private CharacterUpgradeObject restaurantUpgrade;

    [Header("Upgrades")]
    [SerializeField] private int IHateHowHeadersWork;
    [SerializeField] private UnityEvent seatingIncrease, moreTables, qualityStove, restaurantMusic, comfyCouch, selfCheckout;

    private bool seating, tables, cooker, music, couch, checkout;

    public void CheckForUpgrades()
    {
      if(restaurantUpgrade.characterUpgradeChecks[0] && !seating)
      {
        seating = true;
        seatingIncrease.Invoke();
      }
      if(restaurantUpgrade.characterUpgradeChecks[1] && !tables)
      {
        tables = true;
        moreTables.Invoke();
      }
      if(restaurantUpgrade.characterUpgradeChecks[2] && !cooker)
      {
        cooker = true;
        qualityStove.Invoke();
      }
      if(restaurantUpgrade.characterUpgradeChecks[3] && !music)
      {
        music = true;
        restaurantMusic.Invoke();
      }
      if(restaurantUpgrade.characterUpgradeChecks[4] && !couch)
      {
        couch = true;
        comfyCouch.Invoke();
      }
      if(restaurantUpgrade.characterUpgradeChecks[5] && !checkout)
      {
        checkout = true;
        selfCheckout.Invoke();
      }
    }
}
