using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MenuObject : ScriptableObject
{
    public string[] orderTag;
    public float[] orderPrice;
    public GameObject[] orderObject;
}
