using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerIngredientButtons : MonoBehaviour
{
    [SerializeField] private RectTransform button;

    public void UpdateButtonSize()
    {
      button.sizeDelta = new Vector2(200, 200);
    }
}
