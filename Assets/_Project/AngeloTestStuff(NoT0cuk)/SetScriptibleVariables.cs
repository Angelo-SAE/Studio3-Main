using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetScriptibleVariables : MonoBehaviour
{
    [SerializeField] private IngredientObject ingredientItemList;
    [SerializeField] private UnityEvent OnAwake;

    private void Awake()
    {
      ingredientItemList.ingredientCount[0] = 5;
      ingredientItemList.ingredientCount[1] = 5;
      ingredientItemList.ingredientCount[2] = 5;
      ingredientItemList.ingredientCount[3] = 5;
      ingredientItemList.ingredientCount[4] = 5;
      ingredientItemList.ingredientCount[5] = 5;
      ingredientItemList.ingredientCount[6] = 5;



    }

    private void Start()
    {
      OnAwake.Invoke();
    }
}
