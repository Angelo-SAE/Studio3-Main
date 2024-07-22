using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingAssembly : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private int baseArrowSpeed;
    [SerializeField] private IngredientObject cookerStorage;
    [SerializeField] private FloatObject stress;
    [SerializeField] private float stressIncreaseForMiss;
    [SerializeField] private GameObject[] ingredient;
    [SerializeField] private Image[] ingredientSprite;
    [SerializeField] private Image[] failLight;
    [SerializeField] private CookerUI cooker;
    private float ingredientAngle;
    private int direction, arrowSpeed, currentIngredient;
    private bool isAssembling;

    private void Awake()
    {
      direction = -1;
    }

    public void PrepareAssembly()
    {
      gameObject.SetActive(true);
      ResetFailLights();
      ResetIngredients();
      ingredientAngle = Random.Range(50, 320 + 1);
      currentIngredient = 0;
      cookerStorage.count = 0;
      arrowSpeed = baseArrowSpeed;
      ingredient[currentIngredient].transform.eulerAngles = new Vector3(0,0,ingredientAngle);
      ingredientSprite[currentIngredient].sprite = cookerStorage.ingredient[currentIngredient].IngredientSprite;
      ingredient[currentIngredient].SetActive(true);
      isAssembling = true;
    }

    private void ResetFailLights()
    {
      for(int a = 0; a < 3; a++)
      {
        failLight[a].color = new Color(1,1,1,1);
      }
    }

    private void ResetIngredients()
    {
      for(int a = 0; a < ingredient.Length; a++)
      {
        ingredient[a].SetActive(false);
      }
    }

    private void Update()
    {
      if(isAssembling)
      {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
          direction *= -1;
          arrowSpeed += 30;
          CheckForIngredientPress();
        }
        wheel.eulerAngles = new Vector3(0,0,wheel.eulerAngles.z + (direction * Time.deltaTime * (arrowSpeed + (stress.value * 1.5f))));
      }
    }

    private void CheckForIngredientPress()
    {
      if(wheel.eulerAngles.z >  ingredientAngle - 15 && wheel.eulerAngles.z < ingredientAngle + 15)
      {
        ingredient[currentIngredient].SetActive(false);
        currentIngredient++;
        if(currentIngredient < cookerStorage.ingredient.Length)
        {
          if(cookerStorage.ingredient[currentIngredient] is not null)
          {
            RevealNextIngredient();
          } else {
            cooker.Cook();
            isAssembling = false;
            gameObject.SetActive(false);
          }
        } else {
          cooker.Cook();
          isAssembling = false;
          gameObject.SetActive(false);
        }
      } else {
        cookerStorage.count++;
        if(cookerStorage.count > 3)
        {
          cooker.CookSlop();
          isAssembling = false;
          gameObject.SetActive(false);
        } else {
          stress.value += stressIncreaseForMiss;
          failLight[cookerStorage.count - 1].color = new Color(0.68f,0.17f,0.17f,1);
        }
        //what happens when they miss.
      }

    }

    private void RevealNextIngredient()
    {
      ingredientAngle = (wheel.eulerAngles.z + Random.Range(70, 250)) % 360;
      ingredient[currentIngredient].transform.eulerAngles = new Vector3(0,0,ingredientAngle);
      ingredientSprite[currentIngredient].sprite = cookerStorage.ingredient[currentIngredient].IngredientSprite;
      ingredient[currentIngredient].SetActive(true);
    }



    private void Reset()
    {
      direction = -1;
      wheel.eulerAngles = new Vector3(0,0,0);
    }
}
