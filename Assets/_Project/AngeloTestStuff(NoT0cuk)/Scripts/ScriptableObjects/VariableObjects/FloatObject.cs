using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatObject", menuName = "VariableObjects/FloatObject", order = 100)]
public class FloatObject : ScriptableObject
{
    public float value;

    public void SetValue(float newValue)
    {
      value = newValue;
    }
}
