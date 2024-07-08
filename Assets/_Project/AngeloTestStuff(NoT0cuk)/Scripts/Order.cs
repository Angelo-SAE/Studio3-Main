using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Sprite orderSprite;
    [SerializeField] private int orderNumber;

    public Sprite OrderSprite => orderSprite;
    public int OrderNumber  => orderNumber;
}
