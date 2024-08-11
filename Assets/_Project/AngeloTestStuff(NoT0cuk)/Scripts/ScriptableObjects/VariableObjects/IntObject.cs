using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntObject", menuName = "VariableObjects/IntObject", order = 100)]
public class IntObject : ScriptableObject
{
    public int value;

    public void SetValue(int newValue)
    {
      value = newValue;
    }
}
