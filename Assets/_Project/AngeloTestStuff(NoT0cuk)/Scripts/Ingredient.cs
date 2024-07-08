using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Sprite ingredientSprite;
    [SerializeField] private int ingredientNumber;

    public Sprite IngredientSprite => ingredientSprite;
    public int IngredientNumber  => ingredientNumber;
}
