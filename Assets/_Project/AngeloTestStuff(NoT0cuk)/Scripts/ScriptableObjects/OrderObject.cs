using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OrderObject : ScriptableObject
{
    public Order[] order;
    public Order[] pairOrder;
    public bool[] cooked;
    public bool changedOrder;
}
