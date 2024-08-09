using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CharacterUpgradeObject : ScriptableObject
{
    public string[] characterUpgrades;
    public bool[] characterUpgradeChecks;
    public int[] characterUpgradePrice;
    public int inventorySize;
}
