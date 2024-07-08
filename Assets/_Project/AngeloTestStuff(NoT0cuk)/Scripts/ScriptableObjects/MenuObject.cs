using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MenuObject : ScriptableObject
{
    public string[] orderTag;
    public float[] orderPrice;
    public Order[] order;
    public GameObject[] orderObjects;
    public IngredientNumber[] orderIngredients;
}

[System.Serializable]
public struct IngredientNumber
{
  public int[] ingredientNumber;
}
