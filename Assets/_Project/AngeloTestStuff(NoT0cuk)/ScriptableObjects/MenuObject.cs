using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MenuObject : ScriptableObject
{
    public string[] orderTag;
    public float[] orderPrice;
    public GameObject[] orderSprite, orderObjects;
    public FoodNumber[] orderIngredients;
}

[System.Serializable]
public struct FoodNumber
{
  public int[] foodNumber;
}
