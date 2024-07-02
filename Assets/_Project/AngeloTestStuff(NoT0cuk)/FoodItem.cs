using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int foodNumber;

    public Sprite ItemSprite => itemSprite;
    public int FoodNumber  => foodNumber;
}
