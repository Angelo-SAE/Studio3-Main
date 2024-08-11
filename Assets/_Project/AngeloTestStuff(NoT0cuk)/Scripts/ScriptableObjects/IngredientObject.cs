using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IngredientObject : ScriptableObject
{
    public Ingredient[] ingredient;
    public int[] ingredientCount;
    public int[] ingredientPrice;
    public int count;
}
