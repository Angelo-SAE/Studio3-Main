using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOrderRecipe : MonoBehaviour
{
    [SerializeField] private OrderObject orders;
    [SerializeField] private MenuObject menu;
    [SerializeField] private IngredientObject ingredientList;
    [SerializeField] private RectTransform uiToDetect, uiDisplay;
    [SerializeField] private GameObject uiDisplayObject;
    [SerializeField] private Image[] displaySlot;
    [SerializeField] private int tableNumber;
    private bool displaying;
    private Vector2 mousePosition, bounds;
    private int[] slotIngredientNumber;
    private int orderIngredientCount;

    private void Start()
    {
      bounds.x = uiToDetect.sizeDelta.x / 2;
      bounds.y = uiToDetect.sizeDelta.y / 2;
    }

    private void Update()
    {
      GetMousePosition();
      CheckForHover();
    }

    private void GetMousePosition()
    {
      mousePosition = Input.mousePosition;
    }

    private void CheckForHover()
    {
      if(mousePosition.x > uiToDetect.position.x - bounds.x && mousePosition.x < uiToDetect.position.x + bounds.x && mousePosition.y > uiToDetect.position.y - bounds.y && mousePosition.y < uiToDetect.position.y + bounds.y)
      {
        if(orders.order[tableNumber] is not null)
        {
          if(!displaying)
          {
            displaying = true;
            uiDisplayObject.SetActive(true);
          }
          PositionDisplay();
          GetOrderInformation();
          DisplayRecipe();
        }
      } else if(displaying)
      {
        displaying = false;
        uiDisplayObject.SetActive(false);
      }
    }

    private void PositionDisplay()
    {
      uiDisplay.position = new Vector2(mousePosition.x - (uiDisplay.sizeDelta.x / 2), mousePosition.y - (uiDisplay.sizeDelta.y / 2));
    }

    //private void PositionDisplay()
    //{
    //  uiDisplay.position = new Vector2(mousePosition.x - (uiDisplay.sizeDelta.x / 2), mousePosition.y);
    //}

    private void GetOrderInformation()
    {
      slotIngredientNumber = menu.orderIngredients[orders.order[tableNumber].OrderNumber].ingredientNumber;
      orderIngredientCount = slotIngredientNumber.Length;
    }

    private void DisplayRecipe()
    {
      ResetSprites();
      for(int a = 0; a < slotIngredientNumber.Length; a++)
      {
        displaySlot[a].sprite = ingredientList.ingredient[slotIngredientNumber[a]].IngredientSprite;
        displaySlot[a].color = new Color(1,1,1,0.9f);
      }
    }

    private void ResetSprites()
    {
      for(int a = 0; a < orderIngredientCount; a++)
      {
        displaySlot[a].sprite = null;
        displaySlot[a].color = new Color(1,1,1,0);
      }
    }
}
